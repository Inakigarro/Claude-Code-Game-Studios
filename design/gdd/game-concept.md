# Game Concept: The Last Engineers

*Created: 2026-04-26*
*Status: Draft*

---

## Elevator Pitch

> It's a 2D factory-builder roguelite where you optimize steampunk automation systems across generations of engineers — each run you inherit only the knowledge your predecessors earned, growing smarter every life until you finally build the machine that escapes a dying planet.

---

## Core Identity

| Aspect | Detail |
| ---- | ---- |
| **Genre** | Factory-builder / Automation + Roguelite meta-progression |
| **Platform** | PC (Steam) |
| **Target Audience** | Systems achievers; hardcore automation fans; mid-core roguelite players |
| **Player Count** | Single-player |
| **Session Length** | 1–2 hours per run |
| **Monetization** | Premium (one-time purchase) |
| **Estimated Scope** | Large (3–5 years, solo dev) |
| **Comparable Titles** | Factorio, Hades, Satisfactory |

---

## Core Fantasy

You are the latest engineer in a lineage stretching back generations. Your predecessors built, optimized, failed, and died — but they left something behind: their knowledge. The research they unlocked, the technologies they proved, the dead ends they eliminated. You begin each life with the accumulated understanding of everyone who came before you, and a blank factory floor.

The power fantasy is not brute force. It's the feeling of a system working perfectly because *you understood it well enough to make it so*. The fifth-generation engineer who builds in 20 minutes what took the first engineer two hours. The factory that hums with no wasted output, every pipe full, every machine at capacity — running exactly as designed, because you now know exactly how to design it.

---

## Unique Hook

> Like Factorio, **AND ALSO** your factory is a multigenerational project — each run you start smarter because your knowledge carries forward, but your factory doesn't. The planet's dying resources are the clock. Your accumulated expertise is the key.

---

## Player Experience Analysis (MDA Framework)

### Target Aesthetics (What the player FEELS)

| Aesthetic | Priority | How We Deliver It |
| ---- | ---- | ---- |
| **Sensation** (sensory pleasure) | 3 | Steampunk machinery visuals, machine audio feedback, the satisfying hum of a working factory |
| **Fantasy** (make-believe, role-playing) | 4 | Engineer identity; the dying world narrative; steampunk setting |
| **Narrative** (drama, story arc) | 6 | Generational lore fragments; the escape arc; the world's history revealed through research |
| **Challenge** (obstacle course, mastery) | 1 | Systems optimization under depletion pressure; efficiency as survival |
| **Fellowship** (social connection) | N/A | Solo game; no social layer |
| **Discovery** (exploration, secrets) | 2 | Tech tree exploration; emergent factory behavior; lore uncovered through research |
| **Expression** (self-expression, creativity) | 5 | Factory layout as personal design space |
| **Submission** (relaxation, comfort zone) | N/A | Not the target; tension and pressure are intentional |

### Key Dynamics (Emergent player behaviors)

- Players will compare factory layouts across runs, treating earlier runs as drafts
- Players will prioritize research nodes differently each run, creating personal "optimal paths" they refine over time
- Players will pause production to observe flow, diagnosing bottlenecks before adjusting
- Players will calculate depletion rates mentally and race to unlock the next tier before a resource runs dry
- Players will share "escape runs" — the run where they finally got out — as milestone moments

### Core Mechanics (Systems we build)

1. **Factory assembly system** — place, connect, and configure machines on a 2D grid; observe item/fluid flow; adjust for efficiency
2. **Resource depletion model** — every extractable resource has finite yield; depletion rate vs. consumption rate creates the core tension
3. **Research & discovery system** — spend refined outputs to unlock new machines, processes, and technologies; unlocks persist across runs
4. **Run cycle (generational loop)** — each run ends when the escape goal is met OR resource depletion makes it impossible; carry forward unlocked research, reset the factory
5. **Escape engineering arc** — a multi-component, high-resource goal that serves as the long-term target; completing it ends the meta-progression

---

## Player Motivation Profile

### Primary Psychological Needs Served

| Need | How This Game Satisfies It | Strength |
| ---- | ---- | ---- |
| **Autonomy** (freedom, meaningful choice) | Factory layout, research priority order, expansion direction, resource routing strategy | Core |
| **Competence** (mastery, skill growth) | Each run is visibly faster and more efficient; knowledge inheritance proves mastery is real | Core |
| **Relatedness** (connection, belonging) | Minimal — the generational framing creates a weak narrative "connection" to previous engineers | Minimal |

### Player Type Appeal (Bartle Taxonomy)

- [x] **Achievers** (goal completion, collection, progression) — How: research tree completion, escape milestones, efficiency metrics
- [x] **Explorers** (discovery, understanding systems, finding secrets) — How: tech tree depth, emergent factory behavior, generational lore
- [ ] **Socializers** (relationships, cooperation, community) — Not served by this game
- [ ] **Killers/Competitors** (domination, PvP, leaderboards) — Not served by this game

### Flow State Design

- **Onboarding curve**: First run is a guided scarcity experience — limited resources force learning the basics. Tutorial emerges from constraint, not pop-ups.
- **Difficulty scaling**: Each tech tier introduces new resources and machine interactions; depletion pressure scales with run depth; later runs are harder because the escape ship demands more.
- **Feedback clarity**: Visual item flow (items move on belts/pipes), machine status indicators, efficiency counters. Players see exactly where throughput is lost.
- **Recovery from failure**: Run failure is not a setback — it's a research contribution. The player always unlocks at least one node before a run ends. Failure is the price of knowledge.

---

## Core Loop

### Moment-to-Moment (30 seconds)
Place a machine or connection → observe the output flow → identify a bottleneck or inefficiency → adjust placement, routing, or configuration → observe again. The cycle is: **build → watch → diagnose → fix**. Intrinsic satisfaction comes from the cause-and-effect clarity: every adjustment has a visible result. Audio and visual feedback (machine sounds, items flowing, output counters ticking up) reinforce each successful fix.

### Short-Term (5–15 minutes)
Build a complete resource chain from raw extraction to refined output. Each chain is a micro-puzzle: mine the ore → smelt it → process the alloy → feed the assembler. Completing a chain unlocks the "one more connection" impulse — the next chain is now possible. Research points accumulate from outputs, making research unlocks feel earned rather than gated.

### Session-Level (1–2 hours)
A full session spans: start a new run with inherited knowledge → build the initial extraction foundation → hit the first depletion threshold (forcing expansion or optimization) → unlock 1–2 research nodes → push toward the session's escape milestone → reach a natural break (resource crisis resolved, new tech tier opened, or run-end condition approaching). Players leave each session thinking: "I know exactly what I'd do differently."

### Long-Term Progression
Progress is measured in inherited knowledge. Each run adds to the research tree. The meta-progression arc has a definitive end: completing the escape ship. Along the way, players unlock new machine types, new resource chains, new efficiency techniques, and fragments of the world's lore. The game is "done" when the first successful escape is achieved — though replay emerges naturally from the desire to optimize the escape time.

### Retention Hooks
- **Curiosity**: "What does the next research tier unlock?" / "What happened to the previous engineers — why did they fail?"
- **Investment**: Inherited research is permanent; every run adds to a body of knowledge the player owns
- **Mastery**: "I know exactly what I'd do differently" — the game leaves players with a concrete next run in mind
- **Social** (minor): Factory screenshots and "escape run" milestones are naturally shareable

---

## Game Pillars

### Pillar 1: Mastery Is the Currency
The game's core reward is not loot or story — it's the feeling of a system you now understand running cleanly. Every mechanic should ask: "does this make the player feel smarter when they figure it out?"

*Design test*: Feature X gives players a new tool vs. Feature X makes players figure out how to use an existing tool better → choose the latter.

### Pillar 2: Scarcity Is the Storyteller
Resource depletion is not a punishment — it's the narrative engine. The planet dying is why the engineers work. Every resource limit should feel like pressure, not friction.

*Design test*: Does this constraint make the player lean in or step back? → lean in = keep it, step back = redesign it.

### Pillar 3: Every Run Moves the Needle
No run should feel wasted. Even a failed generation should unlock something — a research node, a blueprint theory, a piece of lore. Progress must always be visible.

*Design test*: Can a player articulate what they learned this run? → if not, the run gave them nothing.

### Pillar 4: The Machine Should Be Legible
A player who pauses and looks at their factory should be able to read it — trace the flow from raw resource to output. Complexity is earned, not imposed.

*Design test*: Can a new player watch an experienced player's factory and understand what it does? → if not, it's too opaque.

### Anti-Pillars (What This Game Is NOT)

- **NOT a survival game**: Hunger, health, and base defense are not the point. Adding combat mechanics would shift focus from engineering mastery to reaction time — that's a different game.
- **NOT a narrative game**: Lore exists to give context to scarcity, not to be the main draw. Extensive dialogue trees pull focus from systems design.
- **NOT a sandbox**: There is an escape goal and it has teeth. Making the ending optional removes the meaning from scarcity.

---

## Inspiration and References

| Reference | What We Take From It | What We Do Differently | Why It Matters |
| ---- | ---- | ---- | ---- |
| **Factorio** | Factory assembly, logistics chains, belt routing, resource chain design | Add roguelite meta-progression; hard resource depletion instead of infinite extraction; steampunk aesthetic | Proves the automation core loop has a massive passionate audience |
| **Hades** | "Run failure is character development" philosophy; meta-progression that makes each run feel additive | Knowledge-only carry-over is more constrained — mastery lives in the player, not just the save file | Proves roguelite runs can feel rewarding even when you "lose" |
| **Satisfactory** | Visual resource flow, satisfying factory hum, sense of scale in automation | 2D perspective, steampunk aesthetic, hard resource limits instead of infinite planet | Validates the "automation as power fantasy" appeal to a modern audience |

**Non-game inspirations**: Jules Verne's engineering optimism (the belief that cleverness can solve any problem); Victorian industrial aesthetic (visible machinery, functional beauty); the history of the Industrial Revolution as a period of desperate resource-driven innovation.

---

## Target Player Profile

| Attribute | Detail |
| ---- | ---- |
| **Age range** | 20–35 |
| **Gaming experience** | Mid-core to hardcore |
| **Time availability** | 1–2 hour sessions on evenings/weekends |
| **Platform preference** | PC (Steam) |
| **Current games they play** | Factorio, Satisfactory, Hades, Minecraft |
| **What they're looking for** | An automation game where every run feels like it matters — not just grinding toward a sandbox endpoint |
| **What would turn them away** | Excessive randomness that undermines planning; punishing failure with no compensation; hand-holding tutorials that insult their intelligence |

---

## Visual Identity Anchor

*To be established by `/art-bible`.* The visual direction will be determined after running `/art-bible`, which will produce a named direction, one-line visual rule, 2–3 supporting visual principles with design tests, and a color philosophy.

**Working aesthetic reference points** (from the concept):
- Steampunk machinery — visible gears, pipes, valves, steam; functional beauty
- A dying world — resource-exhausted landscapes, desaturated earth tones, contrast between working machinery (warm light, activity) and depleted extraction sites (dark, still)
- Generational weight — the factory floor tells the story of previous attempts; ruins and salvageable structures from past runs as a potential visual motif

---

## Technical Considerations

| Consideration | Assessment |
| ---- | ---- |
| **Recommended Engine** | Unity (developer's existing choice; strong 2D pipeline, asset store support, C# ecosystem) |
| **Key Technical Challenges** | Entity simulation performance — automation games spawn hundreds of active entities. Standard MonoBehaviour will hit limits; Unity DOTS/ECS is the path forward but adds complexity for a first project. Must be prototyped early. |
| **Art Style** | TBD — to be determined by `/art-bible` |
| **Art Pipeline Complexity** | TBD — depends on art style decision |
| **Audio Needs** | Moderate — steampunk ambient soundscape, machine audio feedback, factory hum that scales with output. Adaptive audio for depletion tension is a target. |
| **Networking** | None — single-player only |
| **Content Volume** | ~20 resource types, ~40 machine types, ~70 research nodes, ~20–40 hours first completion |
| **Procedural Systems** | Resource deposit distribution per run (keeps runs feeling distinct without changing the meta-progression structure) |

---

## Risks and Open Questions

### Design Risks
- **Roguelite balance**: Calibrating what knowledge carries over so runs feel distinct but inheritance feels meaningful — if too much carries over, early runs feel like filler; too little and the meta-progression is invisible.
- **Depletion pacing**: Run length is determined by how fast resources deplete vs. how fast players can upgrade. This needs careful playtesting — too fast is frustrating, too slow removes urgency.
- **First run experience**: Players who haven't played automation games will be overwhelmed; players who have will want to skip onboarding. Tutorial by constraint needs to thread this needle.

### Technical Risks
- **Simulation performance**: Hundreds of concurrent entities (belts, pipes, machines, items in transit) is a known bottleneck for automation games. Unity DOTS/ECS addresses this but is a steep learning curve. Must be prototyped in MVP.
- **Save/load complexity**: Meta-progression (research tree across runs) combined with per-run factory state requires a careful save architecture.

### Market Risks
- **Long development cycles**: Factorio took 8 years. A solo dev building comparable-depth automation over 3–5 years carries execution risk.
- **Genre crowding**: Multiple new automation games release on Steam annually. The roguelite differentiator needs to be immediately legible in store presentation.

### Scope Risks
- **Three complex systems**: Factory-builder + roguelite meta + steampunk art production is three separate complex systems for a first game.
- **Content volume**: 40 machine types and 70 research nodes is substantial solo content production.

### Open Questions
- **Depletion model**: Is depletion per-tile (each deposit site runs out) or global (total resource pool)? Per-tile creates more spatial decisions; global is simpler to balance. Resolve via prototype.
- **Run-end condition**: Does the run end when the escape goal is met, OR when a critical resource hits zero, OR on a timer? Resolve via playtesting.
- **Ruins system**: Do physical structures from previous runs persist as salvageable ruins? Deferred — it would blur the "knowledge only" clarity. Revisit after MVP.

---

## MVP Definition

**Core hypothesis**: "The observe-diagnose-fix automation loop is intrinsically satisfying, and the knowledge-only inheritance system makes failed runs feel rewarding rather than punishing."

**Required for MVP**:
1. Factory assembly on a 2D grid: place/connect 8–10 machine types, basic belt/pipe logistics, observe item flow
2. Resource depletion: 5 resource types with finite deposits; depletion creates visible pressure
3. Research system: 5–8 nodes in 1 tech tier; nodes unlock new machines or improve efficiency; nodes persist across runs
4. Run cycle: run ends when escape component is built OR critical depletion; knowledge carries; factory resets; new run begins
5. One escape component as the run goal — a clear, concrete target to build toward

**Explicitly NOT in MVP**:
- Multiple biomes or areas
- Full tech tree (1 tier only)
- Lore or narrative content
- Art polish or final asset production
- Meta-UI (research tree visualization, run history)
- Procedural resource distribution (fixed deposits for predictability)

### Scope Tiers

| Tier | Content | Features | Timeline (solo) |
| ---- | ---- | ---- | ---- |
| **MVP** | 1 biome, 5 resources, 8–10 machines, 1 tech tier | Core loop + depletion + knowledge carry | 3–6 months |
| **Vertical Slice** | 1 complete biome, 3 resource chains, 2–3 tech tiers | Full run cycle + basic meta-progression + simple escape goal | 12–18 months |
| **Alpha** | All biomes placeholder, full resource set | All features rough, content incomplete | 24–30 months |
| **Full Vision** | Complete content, polished, Steam-ready | All features, adaptive audio, lore, meta-UI | 3–5 years |

---

## Next Steps

- [ ] Run `/setup-engine` to configure Unity and populate version-aware reference docs
- [ ] Run `/art-bible` to establish the Visual Identity Anchor — do this BEFORE writing system GDDs
- [ ] Run `/design-review design/gdd/game-concept.md` to validate concept completeness
- [ ] Run `/map-systems` to decompose the concept into individual systems with dependencies
- [ ] Author per-system GDDs with `/design-system` for: Factory Assembly, Resource Depletion, Research System, Run Cycle, Escape Arc
- [ ] Run `/create-architecture` to produce the master architecture blueprint — simulation performance risk must be in the Required ADR list
- [ ] Record key architectural decisions with `/architecture-decision (×N)`
- [ ] Run `/gate-check` before committing to production
- [ ] Prototype the simulation performance risk with `/prototype simulation-performance`
- [ ] Run `/playtest-report` after MVP prototype to validate the core hypothesis
- [ ] Plan first sprint with `/sprint-plan new`
