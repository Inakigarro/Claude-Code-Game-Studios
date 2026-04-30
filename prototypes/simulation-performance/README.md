# Prototype: Simulation Performance

**Question**: Can Unity 6.3 DOTS/ECS run 100–2000+ concurrent factory entities (belt items +
machines ticking) within a 16.6ms frame budget, and is the stack maintainable for a solo dev?

**Date**: 2026-04-28  
**Status**: In progress  
**Skill**: `/prototype simulation-performance`

---

## Setup Instructions

### 1. Required Packages

Open Unity 6.3 LTS. In **Window → Package Manager**, install:

| Package | Version | Why |
|---------|---------|-----|
| `com.unity.entities` | 1.3.x | DOTS/ECS runtime |
| `com.unity.burst` | included with Entities | Burst compiler |
| `com.unity.collections` | included with Entities | NativeArray, etc. |
| `com.unity.mathematics` | included with Entities | math.cos, float2, etc. |

> The Entities package brings Burst, Collections, and Mathematics automatically.

### 2. Project Files

Copy all `.cs` files from this directory into a new folder inside your Unity project:

```
Assets/
└── Prototype/
    └── SimulationPerformance/
        ├── Components.cs
        ├── BeltMovementSystem.cs
        ├── MachineTickSystem.cs
        ├── SimulationBootstrap.cs
        └── StressTestController.cs
```

No `.asmdef` is required for a prototype — Unity will compile everything together.

### 3. Scene Setup

1. Create a new empty scene: **File → New Scene → Basic (URP)**
2. Create an empty GameObject, name it `StressTestController`
3. Add the `StressTestController` MonoBehaviour component to it
4. Set Target Frame Rate field to `60` (or leave default)
5. Press **Play**

The test runs automatically — no additional setup needed.

### 4. Reading Results

Results appear in the **Game view** via OnGUI overlay:

- Each stage runs for ~5 seconds (300 frames) after a 1-second warmup
- Stages: 100 → 500 → 1000 → 2000 entities (normal validation)
- Then: 4000 → 8000 → 16000 → ... (extreme ceiling test, doubles until budget breaks)
- **PASS** = avg frame time ≤ 16.6ms
- **FAIL** = avg frame time > 16.6ms → test stops, ceiling identified

For deeper analysis, open **Window → Analysis → Profiler** during the run and look for:
- `BeltMovementSystem` — job scheduling overhead + Burst computation
- `MachineTickSystem` — sequential tick overhead
- `EntityManager` — structural change cost (only at stage transitions)

### 5. What This Tests

| Concern | How Tested |
|---------|-----------|
| Entity throughput | N BeltItem entities updated every frame via parallel IJobEntity |
| Machine tick overhead | M MachineData entities updated sequentially via IJobEntity |
| Burst compilation | Both jobs are [BurstCompile] — confirms Burst actually fires |
| DOTS maintainability | Subjective — assess after implementing: can you read/debug it in 30 min? |

### 6. What This Does NOT Test

- Item hand-off between belts (structural changes — separate concern)
- Rendering cost (no sprites used — debug overlay only)
- Save/Load overhead
- Pathfinding or complex routing

---

## Maintainability Checklist (fill in after first run)

After getting the prototype running, answer honestly:

- [ ] Could you understand all 5 files without external docs?
- [ ] Could you add a new component type in < 15 min?
- [ ] Could you debug a wrong entity count using Entities Hierarchy window?
- [ ] Did the Burst Inspector show your jobs actually compiled?
- [ ] Would you be comfortable debugging this alone at 2am during a jam?

Record answers in REPORT.md.
