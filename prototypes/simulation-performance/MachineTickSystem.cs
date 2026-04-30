// PROTOTYPE - NOT FOR PRODUCTION
// Question: Can Unity 6.3 DOTS/ECS run 100-2000+ factory entities at 60fps for a solo dev?
// Date: 2026-04-28

using Unity.Burst;
using Unity.Entities;

namespace Prototype.SimulationPerformance
{
    // Machines tick at their configured rate. In production this would trigger item
    // spawning/despawning (structural changes). Here we just increment a counter to isolate
    // and measure pure tick scheduling overhead independent of structural change cost.
    [BurstCompile]
    public partial struct TickMachinesJob : IJobEntity
    {
        public float DeltaTime;

        public void Execute(ref MachineData machine)
        {
            machine.TickAccumulator += DeltaTime;
            float interval = 1f / machine.TickRate;
            if (machine.TickAccumulator >= interval)
            {
                machine.TickAccumulator -= interval;
                machine.TotalTicks++;
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(BeltMovementSystem))]
    public partial struct MachineTickSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Sequential (Schedule, not ScheduleParallel) — machines will have data
            // dependencies on item counts in production, so single-threaded is realistic.
            state.Dependency = new TickMachinesJob { DeltaTime = SystemAPI.Time.DeltaTime }
                .Schedule(state.Dependency);
        }
    }
}
