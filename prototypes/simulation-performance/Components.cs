// PROTOTYPE - NOT FOR PRODUCTION
// Question: Can Unity 6.3 DOTS/ECS run 100-2000+ factory entities at 60fps for a solo dev?
// Date: 2026-04-28

using Unity.Entities;
using Unity.Mathematics;

namespace Prototype.SimulationPerformance
{
    // Represents one item traveling along a belt (the bulk of factory simulation load).
    // Uses a circular path parameterized by Progress so we can test movement math
    // without implementing a full belt graph — the compute cost per entity is equivalent.
    public struct BeltItemData : IComponentData
    {
        public float Progress;    // 0..1, position along circular route
        public float Speed;       // circuits per second
        public int CircuitIndex;  // which ring this item travels on (visual grouping)
        public float2 Position;   // world-space XY, recomputed by BeltMovementSystem each frame
    }

    // Represents a machine that ticks at a fixed rate.
    // TotalTicks acts as a work counter — simulates processing load without structural changes.
    public struct MachineData : IComponentData
    {
        public float TickRate;        // ticks per second
        public float TickAccumulator; // time since last tick
        public int TotalTicks;        // total ticks completed (proxy for "items produced")
    }
}
