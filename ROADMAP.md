# Roadmap

Broad plan per milestone. Go in depth on each when we get there.

## Before M1
- `git init` + Unity `.gitignore`
- Confirm Input System actions have a `Move` action bound to WASD + stick

## M1 — Cube moves, cubes chase, collision kills
- `PlayerController` — reads Move action from Input System, moves via `Rigidbody2D` velocity
- `EnemyChase` — moves toward player transform each FixedUpdate
- `EnemySpawner` — spawns enemies just outside camera bounds on a timer
- `PlayerHealth` — takes damage on trigger with enemy, dies at 0
- Physics layers: `Player`, `Enemy`, `Projectile`, `Pickup` + collision matrix
- Camera follows player (Cinemachine or a 5-line follow script)
- **Done when:** you can run away, get swarmed, and die

## M2 — Auto-attack + enemy health/death
- `WeaponData` ScriptableObject (damage, cooldown, projectile prefab, count, speed)
- `Weapon` component — fires at nearest enemy on cooldown, one per equipped weapon
- `Projectile` — moves, deals damage on hit, self-destructs
- `EnemyHealth` — HP, `TakeDamage()`, death → despawn
- `EnemyData` SO (HP, speed, damage, XP value)
- **Done when:** enemies die to one auto-firing weapon

## M3 — XP → level up → pick 1 of 3
- `XpGem` pickup prefab dropped on enemy death, magnets toward player in range
- `PlayerLevel` — XP curve, fires `OnLevelUp` event
- `UpgradeData` SO base type (weapon unlock / weapon level / passive stat)
- `UpgradeManager` — picks 3 random valid upgrades, pauses game (`Time.timeScale = 0`)
- `LevelUpUI` — 3 cards, click → apply upgrade, resume
- **Done when:** the core loop closes: kill → XP → level → upgrade → stronger

## M4 — 5 weapons, 10 passives, stacking
- Weapons: projectile, orbiting blade, aura, boomerang, lightning strike — each a `WeaponBehaviour` subclass driven by SO levels
- `PlayerStats` — single stats object (damage%, cooldown%, speed, max HP, pickup radius, projectile count, area, armor, regen, XP gain) that weapons read from
- Passives modify `PlayerStats`; weapon upgrades bump the weapon's level index
- Enemy variety: 3–4 `EnemyData` types, spawner picks by time-based weight table (`WaveData` SO)
- **Done when:** a 10-min run has meaningful build choices

## M5 — Pooling + profiler pass
- Generic `ObjectPool<T>` (or Unity's `ObjectPool`) for enemies, projectiles, XP gems, damage numbers
- Replace every `Instantiate/Destroy` with pool get/release
- Enemy movement: cache transforms, no `FindObjectOfType`, simple spatial grid or `Physics2D.OverlapCircleNonAlloc` for targeting
- Profile at 300+ enemies; fix the top 3 offenders
- **Done when:** 60fps with 300 enemies on screen

## M6 — Menu, pause, game over, high score
- `GameManager` state machine: `Menu → Playing → Paused → GameOver`
- Scenes: `MainMenu`, `Arena` (or one scene with UI panels)
- HUD: HP bar, XP bar, timer, kill count, equipped weapon icons
- `SaveSystem` — JSON to `Application.persistentDataPath` for high score (time survived / kills)
- **Done when:** menu → play → die → score → menu without restarting

## M7 — Art/audio/feel, build, publish
- Swap cubes for sprites (free packs fine), simple animations
- `AudioManager` — SFX pool, music loop
- Feel: hit flash, screen shake on damage, particles on death/pickup, damage numbers, hit-stop
- Camera: replace exact follow with smoothed follow (`Vector3.SmoothDamp` or Cinemachine) — exact follow feels rigid; not visible now with uniform bg
- Build (Windows + WebGL), upload to itch.io, write the page
- **Done when:** the itch link exists
