// PROTOTYPE - NOT FOR PRODUCTION
// Question: Can Unity 6.3 DOTS/ECS run 100-2000+ factory entities at 60fps for a solo dev?
// Date: 2026-04-28

using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Prototype.SimulationPerformance
{
    // Each belt item advances along its circular circuit and updates its world position.
    // ScheduleParallel spreads the work across worker threads — this is the production pattern
    // for bulk entity updates in a factory simulation.
    [BurstCompile]
    public partial struct MoveBeltItemsJob : IJobEntity
    {
        public float DeltaTime;

        public void Execute(ref BeltItemData item)
        {
            item.Progress += item.Speed * DeltaTime;
            if (item.Progress >= 1f)
                item.Progress -= 1f;

            float angle = item.Progress * math.PI2;
            float radius = 2f + item.CircuitIndex * 1.5f;
            item.Position = new float2(math.cos(angle) * radius, math.sin(angle) * radius);
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial struct BeltMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Dependency = new MoveBeltItemsJob { DeltaTime = SystemAPI.Time.DeltaTime }
                .ScheduleParallel(state.Dependency);
        }
    }
}
