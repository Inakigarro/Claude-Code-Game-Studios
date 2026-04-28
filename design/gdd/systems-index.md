# Systems Index: The Last Engineers

> **Status**: Draft
> **Created**: 2026-04-28
> **Last Updated**: 2026-04-28
> **Source Concept**: design/gdd/game-concept.md

---

## Overview

The Last Engineers is built on three interlocking pillars: a factory-builder simulation
(build→watch→diagnose→fix), a resource depletion engine (the dying planet as time
pressure), and a roguelite meta-progression loop (knowledge only carries, factory resets).
These three concerns generate 28 distinct systems spanning simulation, resource management,
meta-progression, persistence, and presentation.

The simulation layer is the highest technical risk — automation games spawn hundreds of
active entities, and the design of the Factory Simulation Engine will constrain the entire
game's scalability. It must be prototyped before any other system is committed to
production. Everything else flows from the simulation working.

Design order follows dependency sort within each priority tier. Foundation systems must be
fully specified before Core Simulation begins; Core Simulation before Game Systems; Game
Systems before Presentation. Independent systems at the same layer can be designed in
parallel.

---

## Systems Enumeration

| # | System Name | Category | Priority | Status | Design Doc | Depends On |
|---|-------------|----------|----------|--------|------------|------------|
| 1 | Factory Assembly System | Factory | MVP | Not Started | — | Grid, Machine, Logistics |
| 2 | Resource Depletion System | Resource | MVP | Not Started | — | Resource Database, Extraction |
| 3 | Research & Discovery System | Progression | MVP | Not Started | — | Resource Database, Factory Simulation Engine |
| 4 | Run Cycle | Progression | MVP | Not Started | — | Run State Manager, Knowledge Carry |
| 5 | Escape Arc System | Progression | MVP | Not Started | — | Machine, Factory Simulation Engine |
| 6 | Grid System *(inferred)* | Foundation | MVP | Not Started | — | — |
| 7 | Resource Database *(inferred)* | Foundation | MVP | Not Started | — | — |
| 8 | Input System *(inferred)* | Foundation | MVP | Not Started | — | — |
| 9 | Audio System *(inferred)* | Foundation | MVP | Not Started | — | — |
| 10 | Item/Entity System *(inferred)* | Simulation | MVP | Not Started | — | Grid |
| 11 | Machine System *(inferred)* | Simulation | MVP | Not Started | — | Grid, Resource Database |
| 12 | Logistics System *(inferred)* | Simulation | MVP | Not Started | — | Grid, Item/Entity |
| 13 | Factory Simulation Engine *(inferred)* | Simulation | MVP | Not Started | — | Item/Entity, Machine, Logistics |
| 14 | Extraction System *(inferred)* | Resource | MVP | Not Started | — | Grid, Resource Database, Item/Entity |
| 15 | Research System *(inferred)* | Progression | MVP | Not Started | — | Resource Database, Factory Simulation Engine |
| 16 | Machine Unlock System *(inferred)* | Progression | MVP | Not Started | — | Research, Machine |
| 17 | Run State Manager *(inferred)* | Progression | MVP | Not Started | — | Resource Depletion, Escape Arc |
| 18 | Knowledge Carry System *(inferred)* | Progression | MVP | Not Started | — | Run State Manager, Research |
| 19 | Save/Load System *(inferred)* | Persistence | MVP | Not Started | — | Factory Simulation Engine, Research, Run State Manager |
| 20 | Camera & Viewport *(inferred)* | UI | MVP | Not Started | — | Grid, Input |
| 21 | Factory Visual Rendering *(inferred)* | UI | MVP | Not Started | — | Grid, Machine, Logistics, Item/Entity |
| 22 | Item Flow Visualization *(inferred)* | UI | MVP | Not Started | — | Item/Entity, Logistics, Factory Visual Rendering |
| 23 | Factory HUD *(inferred)* | UI | MVP | Not Started | — | Resource Depletion, Factory Simulation Engine, Camera |
| 24 | Machine Build UI *(inferred)* | UI | MVP | Not Started | — | Machine Unlock, Grid, Input, Camera |
| 25 | Research UI *(inferred)* | UI | MVP | Not Started | — | Research, Input |
| 26 | Run Summary Screen *(inferred)* | UI | MVP | Not Started | — | Knowledge Carry, Research |
| 27 | Tutorial/Onboarding System *(inferred)* | Polish | Vertical Slice | Not Started | — | Run State Manager, Machine Build UI, Resource Depletion |
| 28 | Procedural Generation *(inferred)* | Polish | Alpha | Not Started | — | Resource Database, Grid |
| 29 | Settings & Accessibility UI *(inferred)* | Polish | Alpha | Not Started | — | Input, Audio |
| 30 | Run History *(inferred)* | Polish | Full Vision | Not Started | — | Save/Load, Run State Manager |

---

## Categories

| Category | Description | Systems in This Game |
|----------|-------------|---------------------|
| **Foundation** | Core infrastructure everything builds on | Grid, Resource Database, Input, Audio |
| **Simulation** | The engine that runs the factory | Item/Entity, Machine, Logistics, Factory Simulation Engine, Extraction |
| **Factory** | Mechanic-level design of building and assembly | Factory Assembly System |
| **Resource** | What flows through the factory and depletes | Resource Depletion System, Extraction System |
| **Progression** | Meta-progression across runs | Research & Discovery, Research System, Machine Unlock, Run Cycle, Run State Manager, Escape Arc, Knowledge Carry |
| **Persistence** | Save state and continuity | Save/Load |
| **UI** | Player-facing displays and interactions | Camera, Factory Visual Rendering, Item Flow Visualization, Factory HUD, Machine Build UI, Research UI, Run Summary Screen |
| **Polish** | Tutorial, procedural, accessibility, meta-UI | Tutorial/Onboarding, Procedural Generation, Settings & Accessibility, Run History |

---

## Priority Tiers

| Tier | Definition | Target Milestone | Design Urgency |
|------|------------|------------------|----------------|
| **MVP** | Required for the core loop to function. Without these, you can't test "is this fun?" | First playable prototype (~3–6 months) | Design FIRST |
| **Vertical Slice** | Required for one complete, polished run. Demonstrates the full experience. | Vertical slice demo (~12–18 months) | Design SECOND |
| **Alpha** | All features present in rough form. Complete mechanical scope, placeholder content OK. | Alpha milestone (~24–30 months) | Design THIRD |
| **Full Vision** | Polish, edge cases, nice-to-haves, and content-complete features. | Beta / Release (~3–5 years) | Design as needed |

---

## Dependency Map

### Foundation Layer (no dependencies — design these first)

1. **Grid System** — defines the coordinate space; every machine, belt, and entity is addressed by this
2. **Resource Database** — defines all resource type vocabulary; every recipe, deposit, and research cost references it
3. **Input System** — prerequisite for every player interaction
4. **Audio System** — machine feedback audio is part of the core loop feel, not a polish concern

### Core Simulation Layer (depends on Foundation)

5. **Item/Entity System** — depends on: Grid
6. **Machine System** — depends on: Grid, Resource Database
7. **Logistics System** — depends on: Grid, Item/Entity

### Factory Engine Layer (depends on Core Simulation)

8. **Factory Simulation Engine** — depends on: Item/Entity, Machine, Logistics ⚠️ *highest-risk system*
9. **Extraction System** — depends on: Grid, Resource Database, Item/Entity
10. **Resource Depletion System** — depends on: Resource Database, Extraction, Factory Simulation Engine

### Game Systems Layer (depends on Factory Engine)

11. **Research System** — depends on: Resource Database, Factory Simulation Engine
12. **Machine Unlock System** — depends on: Research, Machine
13. **Escape Arc System** — depends on: Machine, Factory Simulation Engine
14. **Run State Manager** — depends on: Resource Depletion, Escape Arc
15. **Knowledge Carry System** — depends on: Run State Manager, Research
16. **Save/Load System** — depends on: Factory Simulation Engine, Research, Run State Manager ⚠️ *dual-scope save architecture*

### Presentation Layer (wraps gameplay systems)

17. **Camera & Viewport** — depends on: Grid, Input
18. **Factory Visual Rendering** — depends on: Grid, Machine, Logistics, Item/Entity
19. **Item Flow Visualization** — depends on: Item/Entity, Logistics, Factory Visual Rendering
20. **Factory HUD** — depends on: Resource Depletion, Factory Simulation Engine, Camera
21. **Machine Build UI** — depends on: Machine Unlock, Grid, Input, Camera
22. **Research UI** — depends on: Research, Input
23. **Run Summary Screen** — depends on: Knowledge Carry, Research

### Mechanic Design Layer (design-level GDDs covering player rules)

These five GDDs document the *design intent* of each major mechanic — player rules, edge
cases, player experience goals — and cross-reference the implementation systems above.
They are authored *after* the implementation systems they reference are designed.

24. **Factory Assembly System** — depends on: Grid, Machine, Logistics (design)
25. **Resource Depletion System** (mechanic GDD) — depends on: Extraction, Resource Database (design)
26. **Research & Discovery System** (mechanic GDD) — depends on: Research System (design)
27. **Run Cycle** (mechanic GDD) — depends on: Run State Manager, Knowledge Carry (design)
28. **Escape Arc System** (mechanic GDD) — depends on: Escape Arc System (impl), Run State Manager (design)

### Polish Layer (designed last)

29. **Tutorial/Onboarding System** — depends on: Run State Manager, Machine Build UI, Resource Depletion
30. **Procedural Generation** — depends on: Resource Database, Grid
31. **Settings & Accessibility UI** — depends on: Input, Audio
32. **Run History** — depends on: Save/Load, Run State Manager

---

## Recommended Design Order

*(Dependency-sorted within each priority tier. Independent systems at the same layer
can be designed in parallel. Each GDD should be reviewed before the next begins.)*

| Order | System | Priority | Layer | Agent(s) | Est. Effort |
|-------|--------|----------|-------|----------|-------------|
| 1 | Resource Database | MVP | Foundation | game-designer, systems-designer | M |
| 2 | Grid System | MVP | Foundation | game-designer, systems-designer | M |
| 3 | Input System | MVP | Foundation | game-designer | S |
| 4 | Audio System | MVP | Foundation | audio-director | M |
| 5 | Item/Entity System | MVP | Core Simulation | game-designer, systems-designer | M |
| 6 | Machine System | MVP | Core Simulation | game-designer, systems-designer | L |
| 7 | Logistics System | MVP | Core Simulation | game-designer, systems-designer | L |
| 8 | Factory Simulation Engine | MVP | Factory Engine | technical-director, lead-programmer | L |
| 9 | Extraction System | MVP | Factory Engine | systems-designer | M |
| 10 | Resource Depletion System (impl) | MVP | Factory Engine | game-designer, systems-designer | M |
| 11 | Research System (impl) | MVP | Game Systems | game-designer, systems-designer | M |
| 12 | Machine Unlock System | MVP | Game Systems | game-designer | S |
| 13 | Escape Arc System (impl) | MVP | Game Systems | game-designer | S |
| 14 | Run State Manager | MVP | Game Systems | game-designer | S |
| 15 | Knowledge Carry System | MVP | Game Systems | game-designer | S |
| 16 | Save/Load System | MVP | Game Systems | technical-director, lead-programmer | M |
| 17 | Camera & Viewport | MVP | Presentation | ux-designer | S |
| 18 | Factory Visual Rendering | MVP | Presentation | technical-artist, art-director | M |
| 19 | Item Flow Visualization | MVP | Presentation | technical-artist, game-designer | M |
| 20 | Factory HUD | MVP | Presentation | ux-designer, ui-programmer | M |
| 21 | Machine Build UI | MVP | Presentation | ux-designer | M |
| 22 | Research UI | MVP | Presentation | ux-designer | M |
| 23 | Run Summary Screen | MVP | Presentation | ux-designer, game-designer | S |
| 24 | Factory Assembly System (mechanic) | MVP | Mechanic GDD | game-designer | M |
| 25 | Resource Depletion System (mechanic) | MVP | Mechanic GDD | game-designer, systems-designer | M |
| 26 | Research & Discovery System (mechanic) | MVP | Mechanic GDD | game-designer | M |
| 27 | Run Cycle (mechanic) | MVP | Mechanic GDD | game-designer | M |
| 28 | Escape Arc System (mechanic) | MVP | Mechanic GDD | game-designer | S |
| 29 | Tutorial/Onboarding System | Vertical Slice | Polish | ux-designer, game-designer | M |
| 30 | Procedural Generation | Alpha | Polish | game-designer, systems-designer | M |
| 31 | Settings & Accessibility UI | Alpha | Polish | ux-designer, accessibility-specialist | S |
| 32 | Run History | Full Vision | Polish | game-designer, ux-designer | S |

*Effort: S = 1 session, M = 2–3 sessions, L = 4+ sessions.*

---

## Circular Dependencies

**None found.** All 32 design documents form a clean directed acyclic graph. The Factory
Simulation Engine, Research System, and Run State Manager are high-dependency nodes but
do not form cycles.

---

## High-Risk Systems

| System | Risk Type | Risk Description | Mitigation |
|--------|-----------|-----------------|------------|
| **Factory Simulation Engine** | Technical | Hundreds of concurrent entities (items on belts, machines ticking) will exceed MonoBehaviour capacity. Unity DOTS/ECS is the path but has a steep learning curve for a first project. | **Prototype immediately.** Run `/prototype simulation-performance` before any other implementation. ADR required before design begins. |
| **Resource Depletion System** | Design | Depletion rate vs. run length calibration is the core game-feel question. Too fast = frustrating. Too slow = no urgency. | Parametrize all tuning knobs. Multiple playtests required. Formulas must be in the GDD with example calculations. |
| **Save/Load System** | Technical | Meta-progression (research, cross-run) and per-run factory state are two separate data scopes with different reset semantics. Conflating them creates bugs that corrupt the generational loop. | Architecture ADR required before implementation. Design the data model explicitly in the GDD. |
| **Factory Assembly System** | Scope | Full Vision content volume (40 machine types) is a major solo production commitment. If MVP 8–10 machines don't validate the loop, the scope is unjustified. | Validate core loop satisfaction with 8–10 machines before committing to full content volume. |
| **Logistics System** | Design | Belt/pipe throughput rules (capacity, routing priority, deadlock behavior) are complex to design and critical to get right — every factory design expresses opinions about logistics. | Study Factorio's belt rules explicitly. Resolve deadlock semantics before implementation begins. |

---

## Progress Tracker

| Metric | Count |
|--------|-------|
| Total systems identified | 32 (incl. mechanic GDDs) |
| Design docs started | 0 |
| Design docs reviewed | 0 |
| Design docs approved | 0 |
| MVP systems designed | 0 / 28 |
| Vertical Slice systems designed | 0 / 1 |
| Alpha systems designed | 0 / 2 |
| Full Vision systems designed | 0 / 1 |

---

## Next Steps

- [ ] Run `/prototype simulation-performance` — validate entity simulation approach before committing to DOTS/ECS
- [ ] Run `/architecture-decision` to record the simulation engine approach (DOTS vs. MonoBehaviour) as an ADR
- [ ] Begin GDD authoring in design order: start with **Resource Database** → run `/design-system resource-database`
- [ ] Run `/design-review design/gdd/[system].md` after each GDD is authored
- [ ] Run `/gate-check pre-production` when all MVP GDDs are designed and reviewed
- [ ] Run `/create-architecture` to produce the master architecture blueprint (simulation performance ADR must be in the required list)
