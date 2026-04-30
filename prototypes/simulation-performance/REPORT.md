# Prototype Report: Simulation Performance

**Date**: 2026-04-28  
**Tester**: [your name]  
**Machine**: [CPU / RAM / GPU]  
**Unity Version**: 6.3 LTS  
**Entities Package**: 1.3.x

---

## Hypothesis

Unity 6.3 DOTS/ECS with Burst-compiled parallel IJobEntity can process 1000+ concurrent
factory entities (belt items in motion + machines ticking) within a 16.6ms frame budget,
making it a viable foundation for The Last Engineers' factory simulation layer.

---

## Approach

- **BeltItemData entities**: circular-path movement updated via `IJobEntity.ScheduleParallel()`
- **MachineData entities**: fixed-rate tick counter updated via `IJobEntity.Schedule()`
- **No rendering**: raw simulation cost only (no sprites, no physics)
- **Measurement**: VSync disabled, frame rate uncapped; 300-frame window after 60-frame warmup
- **Stages**: 100 → 500 → 1000 → 2000 (normal), then 4000 → 8000 → 16000+ (extreme ceiling)

---

## Results

| Items | Machines | Avg (ms) | Min (ms) | Max (ms) | Result |
|-------|----------|----------|----------|----------|--------|
| 100   |    5     | 1.57     | 1.19     | 23.54    | PASS   |
| 500   |   25     | 1.19     | 1.10     | 1.56     | PASS   |
| 1000  |   50     | 1.21     | 1.13     | 1.80     | PASS   |
| 2000  |  100     | 1.23     | 1.14     | 1.77     | PASS   |
| 4000  |  200     | 1.26     | 1.16     | 1.84     | PASS   |
| 8000  |  400     | 1.32     | 1.18     | 2.01     | PASS   |
| 16000 |  800     | 1.29     | 1.19     | 1.73     | PASS   |
| 32000 | 1600     | 1.31     | 1.21     | 2.06     | PASS   |
| 64000 | 3200     | 1.33     | 1.23     | 1.96     | PASS   |
| 128000| 6400     | 1.39     | 1.24     | 2.21     | PASS   |
| ...   | ...      | ...      | ...      | ...      | PASS   |
| 16384000| 819200 | 21.56    | 4.25     | 38.27    | FAIL   |

*Fill in from the OnGUI overlay after running the test.*

---

## Metrics

- **Frame time at 1000 entities**: 1.21 ms
- **Frame time at 2000 entities**: 1.23 ms
- **Performance ceiling**: ~8192000 entities at ≤16.6ms
- **Breaks at**: 16284000 entities
- **Burst confirmed**: [X] Yes — checked Burst Inspector, jobs compiled  / [ ] No

---

## Maintainability Assessment

*Fill in after getting the prototype running. Answer honestly.*

| Question | Answer |
|----------|--------|
| Understood all 5 files without external docs? | No |
| Could add a new component type in < 15 min? | No | 
| Could debug entity count via Entities Hierarchy? | Yes |
| Burst Inspector confirmed jobs compiled? | Yes |
| Comfortable maintaining this alone at 2am? | No |

**Subjective difficulty** (1 = trivial, 5 = overwhelming): 2/5

---

## Recommendation: PROCEED - DOTS/ECS is performant and viable for production, but requires significant learning investment.

---

## If Proceeding

*Complete this section if recommendation is PROCEED.*

- Architecture requirements for production:
  - [x] Item hand-off: EntityCommandBuffer — patrón correcto, aprender junto con DOTS
  - [x] Belt graph: entity reference en BeltData — simple, suficiente
  - [x] Rendering: sprite sync manual (ECS sim + GameObject sprites)
  - [x] Physics: bypass para belts — rutas matemáticas, Physics 2D solo para el jugador

- Performance targets confirmed:
  - [X] 1000 entities ≤ 5ms (leaves 11.6ms for rendering, UI, audio)
  - [X] 2000 entities ≤ 10ms (tight but feasible)

- Estimated production effort for Factory Simulation Engine: — weeks

---

## Lessons Learned

- Burst + IJobEntity.ScheduleParallel() escala casi linealmente hasta millones de entidades;
  el ceiling (~8M) está órdenes de magnitud por encima de las necesidades del juego.
- El overhead de DOTS (bootstrap, archetype setup) es real pero amortizado — el costo 
  por entidad es mínimo una vez que el sistema está corriendo.
- La complejidad de DOTS no está en el código sino en el modelo mental (entidades, 
  archetypes, SystemState). Requiere inversión de aprendizaje antes de producción.

