# WaveSurvivalArena

Wave-survival arena game (Vampire Survivors–style, 2D top-down). Learning project — one scene, one loop, but it forces every core system. **Rule: finish and publish it, even ugly.** Shipping is the skill.

## Tech
- Unity 6000.3.10f1 (Unity 6), URP 2D, new Input System (`Assets/InputSystem_Actions.inputactions`)
- C# scripts in `Assets/Script/`, prefabs in `Assets/Prefabs/`, SO data in `Assets/ScriptableObjects/`, scenes in `Assets/Scenes/`
- No git repo yet

## Systems to implement
- Player movement + input system
- Enemy spawner with scaling difficulty over time
- Object pooling (200+ enemies will hit perf walls — that's the lesson)
- ScriptableObjects for weapon/upgrade data
- Level-up + upgrade selection UI
- Collision layers, damage, i-frames
- Save/load for high scores
- Audio, particles, screen shake (game feel)

## Milestones
1. Cube moves, cubes chase it, collision kills
2. Auto-attack weapon + enemy health/death
3. XP drops → level up → pick 1 of 3 upgrades
4. 5 weapons, 10 passive upgrades, stacking effects
5. Pooling + profiler pass
6. Menu, pause, game over, high score
7. Art/audio pass, build, publish to itch.io

## Conventions
- Keep it simple; placeholder art (cubes/sprites) until milestone 7
- Data-driven via ScriptableObjects, not hardcoded stats
- Use the Input System package (not legacy `Input.GetAxis`)
- Prefer plain MonoBehaviours; no heavy frameworks
- Milestone-by-milestone plan lives in `ROADMAP.md`
