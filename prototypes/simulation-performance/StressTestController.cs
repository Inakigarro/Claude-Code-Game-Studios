// PROTOTYPE - NOT FOR PRODUCTION
// Question: Can Unity 6.3 DOTS/ECS run 100-2000+ factory entities at 60fps for a solo dev?
// Date: 2026-04-28

using System.Collections.Generic;
using UnityEngine;

namespace Prototype.SimulationPerformance
{
    // Drives the automated stress test sequence and displays live results via OnGUI.
    //
    // Stage sequence:
    //   Normal:  100 -> 500 -> 1000 -> 2000 entities (validates the 60fps budget)
    //   Extreme: 4000 -> 8000 -> 16000 -> ... (doubles until budget breaks, finds ceiling)
    //
    // Each stage: 60-frame warmup (discarded) + 300-frame measurement window.
    // VSync and frame-rate cap are disabled so frame time reflects raw simulation cost.
    public class StressTestController : MonoBehaviour
    {
        [Header("Normal Test Stages")]
        [SerializeField] private int[] _stageItemCounts = { 100, 500, 1000, 2000 };

        [Header("Measurement")]
        [SerializeField] private int _warmupFrames = 60;
        [SerializeField] private int _measureFrames = 300;

        // 1 machine per N belt items — keeps machine count proportional and realistic
        private const int ItemsPerMachine = 20;
        private const float BudgetMs = 16.6f;

        private enum TestPhase { Warmup, Measuring, Complete }

        private int _currentStage = -1;
        private int _currentItemCount = 0;
        private bool _extremeMode = false;
        private TestPhase _phase = TestPhase.Warmup;
        private int _frameCount = 0;

        private float _sumMs;
        private float _minMs;
        private float _maxMs;

        private readonly List<StageResult> _results = new();

        private GUIStyle _labelStyle;
        private GUIStyle _boldStyle;

        private struct StageResult
        {
            public int ItemCount;
            public int MachineCount;
            public float AvgMs;
            public float MinMs;
            public float MaxMs;
            public bool Passed;
        }

        private void Start()
        {
            // Uncap frame rate so we measure raw simulation cost, not vsync sleeping.
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0;

            _labelStyle = new GUIStyle { richText = true, normal = { textColor = Color.white }, fontSize = 13 };
            _boldStyle = new GUIStyle { richText = true, normal = { textColor = Color.white }, fontSize = 14, fontStyle = FontStyle.Bold };

            AdvanceToNextStage();
        }

        private void Update()
        {
            if (_phase == TestPhase.Complete) return;

            float frameMs = Time.unscaledDeltaTime * 1000f;
            _frameCount++;

            if (_phase == TestPhase.Warmup)
            {
                if (_frameCount >= _warmupFrames)
                    BeginMeasurement();
                return;
            }

            // Measuring phase
            _sumMs += frameMs;
            _minMs = Mathf.Min(_minMs, frameMs);
            _maxMs = Mathf.Max(_maxMs, frameMs);

            if (_frameCount >= _measureFrames)
            {
                RecordResult();
                AdvanceToNextStage();
            }
        }

        private void BeginMeasurement()
        {
            _phase = TestPhase.Measuring;
            _frameCount = 0;
            _sumMs = 0f;
            _minMs = float.MaxValue;
            _maxMs = 0f;
        }

        private void RecordResult()
        {
            float avg = _sumMs / _measureFrames;
            _results.Add(new StageResult
            {
                ItemCount = _currentItemCount,
                MachineCount = MachineCountFor(_currentItemCount),
                AvgMs = avg,
                MinMs = _minMs,
                MaxMs = _maxMs,
                Passed = avg <= BudgetMs
            });
        }

        private void AdvanceToNextStage()
        {
            if (_extremeMode)
            {
                // Stop as soon as a stage exceeds the budget
                if (_results.Count > 0 && !_results[_results.Count - 1].Passed)
                {
                    EndTest();
                    return;
                }
                _currentItemCount *= 2;
            }
            else
            {
                _currentStage++;

                if (_currentStage >= _stageItemCounts.Length)
                {
                    // All normal stages done — switch to extreme ceiling hunt
                    _extremeMode = true;
                    _currentItemCount = _stageItemCounts[_stageItemCounts.Length - 1] * 2;
                }
                else
                {
                    _currentItemCount = _stageItemCounts[_currentStage];
                }
            }

            int machines = MachineCountFor(_currentItemCount);
            SimulationBootstrap.SpawnEntities(_currentItemCount, machines);

            _phase = TestPhase.Warmup;
            _frameCount = 0;
        }

        private void EndTest()
        {
            _phase = TestPhase.Complete;
            SimulationBootstrap.ClearAllEntities();
            Debug.Log("[StressTest] Complete. See OnGUI overlay for results.");
        }

        private static int MachineCountFor(int itemCount) => Mathf.Max(1, itemCount / ItemsPerMachine);

        private void OnGUI()
        {
            var area = new Rect(10, 10, 460, Screen.height - 20);
            GUILayout.BeginArea(area);

            GUILayout.Label("PROTOTYPE — Simulation Performance", _boldStyle);
            GUILayout.Label("Unity 6.3 DOTS/ECS | Belt Items + Machine Ticks", _labelStyle);
            GUILayout.Space(6);

            if (_phase != TestPhase.Complete)
            {
                string stageName = _extremeMode
                    ? $"EXTREME  (doubles until budget breaks)"
                    : $"Normal stage {_currentStage + 1} / {_stageItemCounts.Length}";

                string phaseLabel = _phase == TestPhase.Warmup
                    ? $"Warmup  {_frameCount} / {_warmupFrames}"
                    : $"Measuring  {_frameCount} / {_measureFrames}";

                int machines = MachineCountFor(_currentItemCount);
                GUILayout.Label($"Stage:    {stageName}", _labelStyle);
                GUILayout.Label($"Entities: {_currentItemCount:N0} items  +  {machines:N0} machines", _labelStyle);
                GUILayout.Label($"Phase:    {phaseLabel}", _labelStyle);
                GUILayout.Label($"Frame:    {Time.unscaledDeltaTime * 1000f:F2} ms  ({1f / Time.unscaledDeltaTime:F0} fps)", _labelStyle);
            }
            else
            {
                GUILayout.Label("<color=yellow>TEST COMPLETE</color>", _boldStyle);
            }

            GUILayout.Space(10);
            GUILayout.Label($"{'_', 0}".PadRight(55, '_'), _labelStyle);
            GUILayout.Space(4);
            GUILayout.Label($"{"Items",-10} {"Machines",-10} {"Avg ms",-10} {"Min",-8} {"Max",-8} {"Result"}", _labelStyle);

            foreach (var r in _results)
            {
                string color = r.Passed ? "lime" : "red";
                string badge = r.Passed ? "PASS" : "FAIL";
                GUILayout.Label(
                    $"<color={color}>{r.ItemCount,-10:N0} {r.MachineCount,-10:N0} " +
                    $"{r.AvgMs,-10:F2} {r.MinMs,-8:F2} {r.MaxMs,-8:F2} [{badge}]</color>",
                    _labelStyle);
            }

            if (_phase == TestPhase.Complete && _results.Count > 0)
            {
                GUILayout.Space(10);

                int lastPassCount = 0;
                int firstFailCount = 0;
                foreach (var r in _results)
                {
                    if (r.Passed) lastPassCount = r.ItemCount;
                    else if (firstFailCount == 0) firstFailCount = r.ItemCount;
                }

                if (lastPassCount > 0)
                    GUILayout.Label($"<color=lime>CEILING:   ~{lastPassCount:N0} entities within 16.6ms budget</color>", _boldStyle);
                if (firstFailCount > 0)
                    GUILayout.Label($"<color=red>BREAKS AT: {firstFailCount:N0} entities exceeds budget</color>", _boldStyle);

                GUILayout.Space(6);
                GUILayout.Label("Open REPORT.md and fill in the results.", _labelStyle);
            }

            GUILayout.EndArea();
        }

        private void OnDestroy()
        {
            SimulationBootstrap.ClearAllEntities();
        }
    }
}
