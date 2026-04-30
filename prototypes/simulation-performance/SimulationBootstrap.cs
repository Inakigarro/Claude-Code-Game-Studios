// PROTOTYPE - NOT FOR PRODUCTION
// Question: Can Unity 6.3 DOTS/ECS run 100-2000+ factory entities at 60fps for a solo dev?
// Date: 2026-04-28

using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Prototype.SimulationPerformance
{
    public static class SimulationBootstrap
    {
        // itemCount  — number of BeltItem entities (the primary load driver)
        // machineCount — number of Machine entities (secondary, scales with items)
        public static void SpawnEntities(int itemCount, int machineCount)
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("[SimulationBootstrap] No default DOTS world found. " +
                               "Ensure the Entities package is installed.");
                return;
            }

            var em = world.EntityManager;

            DestroyAllPrototypeEntities(em);

            SpawnBeltItems(em, itemCount, machineCount);
            SpawnMachines(em, machineCount);

            Debug.Log($"[SimulationBootstrap] Spawned {itemCount} belt items + {machineCount} machines.");
        }

        public static void ClearAllEntities()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null) return;
            DestroyAllPrototypeEntities(world.EntityManager);
        }

        private static void DestroyAllPrototypeEntities(EntityManager em)
        {
            var itemQuery = em.CreateEntityQuery(typeof(BeltItemData));
            em.DestroyEntity(itemQuery);
            itemQuery.Dispose();

            var machineQuery = em.CreateEntityQuery(typeof(MachineData));
            em.DestroyEntity(machineQuery);
            machineQuery.Dispose();
        }

        private static void SpawnBeltItems(EntityManager em, int itemCount, int machineCount)
        {
            var archetype = em.CreateArchetype(typeof(BeltItemData));
            var entities = new NativeArray<Entity>(itemCount, Allocator.Temp);
            em.CreateEntity(archetype, entities);

            // Distribute items evenly across circuits. Each circuit maps to one machine.
            // Items on the same circuit start at evenly-spaced offsets so no item clusters.
            int circuits = Mathf.Max(1, machineCount);
            int itemsPerCircuit = Mathf.Max(1, itemCount / circuits);

            for (int i = 0; i < itemCount; i++)
            {
                int circuitIndex = i / itemsPerCircuit;
                int positionInCircuit = i % itemsPerCircuit;
                float progress = (float)positionInCircuit / itemsPerCircuit;

                // Slight speed variation per circuit to stress different update patterns
                float speed = 0.15f + (circuitIndex % 6) * 0.02f;

                em.SetComponentData(entities[i], new BeltItemData
                {
                    Progress = progress,
                    Speed = speed,
                    CircuitIndex = circuitIndex,
                    Position = Unity.Mathematics.float2.zero
                });
            }

            entities.Dispose();
        }

        private static void SpawnMachines(EntityManager em, int machineCount)
        {
            var archetype = em.CreateArchetype(typeof(MachineData));
            var entities = new NativeArray<Entity>(machineCount, Allocator.Temp);
            em.CreateEntity(archetype, entities);

            for (int i = 0; i < machineCount; i++)
            {
                em.SetComponentData(entities[i], new MachineData
                {
                    TickRate = 0.5f + (i % 8) * 0.25f, // 0.5 to 2.25 ticks/sec variation
                    TickAccumulator = 0f,
                    TotalTicks = 0
                });
            }

            entities.Dispose();
        }
    }
}
