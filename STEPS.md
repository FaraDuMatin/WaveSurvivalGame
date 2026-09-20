# Steps log

Concise log of every action taken. Newest at bottom.

## Setup
- git init + Unity .gitignore
- CLAUDE.md, ROADMAP.md
- MCP: context7 (.mcp.json), CoplayDev unity-mcp v10.2 (user scope, http 127.0.0.1:8080)
- test: read console, created/deleted TestCube

## M1
- layers: Player (8), Enemy (9)
- 2D collision matrix: Enemy x Enemy = off
- script: PlayerController.cs (reads Move action → rb.linearVelocity)
- Player GO: pos 0,0, tag Player, layer Player
- Player: added Rigidbody2D (gravity 0, freeze rot, continuous), BoxCollider2D, PlayerController
- PlayerController.move ← InputSystem_Actions "Player/Move"
- saved scene
- commit+push: M1 player movement
- CLAUDE.md: STEPS rule → never skip a step
- tested: WASD moves player ✓
- script: CameraFollow.cs (LateUpdate, copy target xy)
- Main Camera: added CameraFollow, target ← Player
- saved scene
- ROADMAP M7: note → smooth camera later (exact follow feels rigid)
- script: Enemy.cs (find Player by tag, velocity toward it)
- script: EnemySpawner.cs (timer, spawn prefab on circle radius 12 around player)
- prefab: Prefabs/Enemy.prefab (red square, layer Enemy, rb gravity 0 freeze rot, BoxCollider2D trigger, Enemy)
- EnemySpawner GO: added EnemySpawner, enemyPrefab ← Enemy.prefab
- saved scene
- tested: enemies spawn + chase ✓
- script: PlayerHealth.cs (hp 100, -20/s while enemy trigger stays, Die → log + timeScale 0)
- tag: Enemy
- Enemy.prefab: tag Enemy
- Player: added PlayerHealth
- saved scene
- commit+push: M1 complete

## M2
- script: WeaponData.cs (SO: damage, cooldown, projectileSpeed, range, projectilePrefab)
- script: EnemyHealth.cs (hp 30, TakeDamage → Destroy at 0)
- script: Projectile.cs (Init dir/speed/dmg, 3s life, on EnemyHealth trigger → damage + destroy)
- script: Weapon.cs (cooldown timer, OverlapCircleAll nearest enemy, spawn projectile)
- layer: Projectile (10)
- 2D collision matrix: Projectile x Player/Projectile/Default = off
- prefab: Prefabs/Projectile.prefab (yellow square 0.3, layer Projectile, rb kinematic, trigger, Projectile)
- SO: ScriptableObjects/BasicShot.asset (defaults, prefab ← Projectile)
- Enemy.prefab: added EnemyHealth
- Player: added Weapon, data ← BasicShot, enemyMask ← Enemy
- saved scene
- tested: enemies die to auto-fire ✓
- commit+push: M2 complete

## M3
- script: PlayerStats.cs (damageMult, cooldownMult, moveSpeed, maxHp, pickupRadius)
- script: UpgradeData.cs (SO: title, Stat enum, amount, Apply(stats))
- script: PlayerLevel.cs (xp curve base 10 ×1.3, OnLevelUp event)
- script: XpGem.cs (magnet in pickupRadius, AddXp on reach, destroy)
- script: UpgradeManager.cs (on level up: pick 3 random, show panel, timeScale 0; click → Apply, resume)
- PlayerController: speed ← stats.moveSpeed
- PlayerHealth: hp ← stats.maxHp
- Weapon: cooldown × stats.cooldownMult, damage × stats.damageMult
- EnemyHealth: gemPrefab, spawn gem on death
- prefab: Prefabs/XpGem.prefab (green 0.25 square, XpGem, no physics)
- Enemy.prefab: gemPrefab ← XpGem
- SO: Upgrade_Damage/Cooldown/Speed/Max/Pickup .asset
- Player: added PlayerStats, PlayerLevel
- UI: Canvas > LevelUpPanel (dark overlay) > Button0-2 (legacy Text)
- EventSystem + InputSystemUIInputModule
- UpgradeManager GO: pool ← 5 upgrades, panel, buttons
- saved scene
- renamed upgrades: Cooldown → "Fire rate +10%", Pickup → "Magnet range +1"
- tested: XP → level up → pick upgrade ✓
- commit+push: M3 complete

## M4a — enemy variety + difficulty
- script: EnemyData.cs (SO: hp, speed, dps, xp, scale, color, unlockTime)
- Enemy.cs: merged EnemyHealth into it, Init(EnemyData), reads Data.speed, drops gem w/ Data.xp, Alive++/-- on enable/disable
- deleted EnemyHealth.cs
- EnemySpawner.cs: interval lerps 1→0.2s over 300s, maxAlive 300, picks random unlocked type
- Projectile: EnemyHealth → Enemy
- XpGem: value public
- PlayerHealth: dps ← Enemy.Data.damagePerSecond
- SO: Enemy_Grunt (30hp, 2spd, t0), Enemy_Runner (15hp, 4spd, small orange, t60), Enemy_Tank (150hp, 1.2spd, big purple, t120)
- Enemy.prefab: gemPrefab ← XpGem, removed missing script slot
- EnemySpawner: types ← 3 enemies
- saved scene
- EnemySpawner.maxAlive 300→100
- BasicShot.damage 10→30
- Enemy_Runner.unlockTime 60→10
- saved scene
- Enemy_Tank.unlockTime 120→30 (test)
- BUG: Enemy.gemPrefab unassigned on prefab (lost when missing-script slot removed) → enemies never died
- Enemy.prefab: gemPrefab ← XpGem (re-assigned, verified)
- tested: 3 enemy types, ramp, cap ✓
- commit+push: M4a

## M4b — weapon architecture
- WeaponData.cs: + title, weaponPrefab, maxLevel 5, damagePerLevel 0.25
- Weapon.cs: now abstract base (Init, Level, Damage/Cooldown props, Update timer → Fire(), Nearest()); enemyMask hardcoded layer 9
- script: ShotWeapon.cs (Weapon subclass, fires projectile at nearest)
- script: WeaponHolder.cs (starting weapon, list, Get/Add/Upgrade)
- UpgradeData.cs: + weapon field, Title(holder), Available(holder), Apply(holder, stats)
- UpgradeManager.cs: filters Available, 3 unique picks, hides unused buttons
- prefab: Prefabs/Weapon_Shot.prefab (empty + ShotWeapon)
- BasicShot: title "Shot", weaponPrefab ← Weapon_Shot
- Player: removed Weapon, added WeaponHolder (starting ← BasicShot)
- Player: stuck abstract Weapon slot → removed by editing Game.unity YAML, reloaded scene
- SO: Upgrade_WShot.asset (weapon ← BasicShot)
- UpgradeManager.pool: + Upgrade_WShot
- saved scene
- WeaponData: + extraProjectileAtLevels {2,4}, spreadAngle 15
- Weapon.cs: + Count prop (1 + levels reached)
- ShotWeapon: fires Count projectiles in a fan
- tested: weapon holder, Shot levels, multi-projectile ✓
- commit+push: M4b

## M4c — 5 weapons
- WeaponData: + rangePerLevel
- Weapon.cs: + Range prop, Tick() virtual called from Update
- script: Hitbox.cs (trigger stay → Enemy.TakeDamage, per-enemy rehit delay 0.5)
- script: OrbitWeapon.cs (Count blades as children at Range, rotate 180°/s, refresh dmg each cooldown)
- script: AuraWeapon.cs (OverlapCircle all in Range every cooldown, circle visual scaled to Range)
- script: Boomerang.cs (out 0.6s then home to owner, spin, destroy on return) + BoomerangWeapon.cs (fan like Shot)
- script: LightningWeapon.cs (Count random enemies in Range, instant dmg, 0.15s bolt fx)
- prefabs: Blade (cyan, hitbox), AuraVisual (blue circle a.25, order -1), BoomerangProj (magenta, hitbox+Boomerang), LightningFX (white bar)
- prefabs: Weapon_Orbit/Aura/Boomerang/Lightning
- SO: Weapon_Orbit (15dmg, r1.5, +blade lv2-5), Weapon_Aura (8dmg/0.5s, r2, +15% r/lv), Weapon_Boomerang (25dmg, 2s, +1 lv3,5), Weapon_Lightning (40dmg, 1.5s, r9, +1 lv2-5)
- SO: Upgrade_WOrbit/WAura/WBoomerang/WLightning
- UpgradeManager.pool: + 4 weapon upgrades (10 total)
- (was in play mode during asset creation → stopped, re-applied pool, saved)
- saved scene
- tested: 5 weapons ✓ (fun)
- commit+push: M4c

## M4d — 10 passives
- PlayerStats: + armor, regen, xpMult, extraProjectiles, areaMult
- Stat enum: + Armor, Regen, XpGain, Projectiles, Area; Apply handles them; MaxHp also heals
- Weapon.cs: Range × areaMult, Count + extraProjectiles
- PlayerLevel: xp × xpMult
- PlayerHealth.cs: rewritten — regen in Update, Heal(), armor subtracts from enemy dps
- SO: Upgrade_Armor (+5), Upgrade_Regen (+1/s), Upgrade_XP (+20%), Upgrade_1 (+1 proj), Upgrade_Area (+15%)
- UpgradeManager.pool: + 5 (15 total: 10 passive + 5 weapon)
- stopped play mode (user was playing), saved scene
- commit+push: M4 complete

## M5 — Pooling + profiler
- script: Pool.cs (static, Dictionary<prefab, Stack>, Get/Release, double-release guard, clears on scene unload)
- script: AutoRelease.cs (timed Pool.Release, replaces Destroy(go, t))
- Enemy: gem via Pool.Get, death → Pool.Release
- EnemySpawner/ShotWeapon/BoomerangWeapon/LightningWeapon: Instantiate → Pool.Get
- Projectile: Destroy → Pool.Release (life via AutoRelease)
- Boomerang: reset t in Init, Destroy → Pool.Release
- XpGem: static cached player/stats/level, Destroy → Pool.Release
- Hitbox: clear lastHit OnEnable
- Weapon: OverlapCircleAll → Physics2D.OverlapCircle(filter, list) non-alloc, shared InRange() list
- Aura/Lightning: use InRange()
- prefabs: Projectile + AutoRelease(3s), LightningFX + AutoRelease(0.15s)
- script: PerfLog.cs (avg/worst ms every 3s), on EnemySpawner, disabled by default
- stress test: 300 enemies + 5 weapons → ~5-6ms/frame (170+fps) in editor; 301 enemy instances total (pool reuse ✓)
- note: editor idles when unfocused (runInBackground off) — focus Unity while testing
- saved scene
- commit+push: M5
- removed PerfLog; script: DebugHud.cs (OnGUI top-right: alive, pool instances, fps; F = spawn 300)
- EnemySpawner: Spawn() extracted, + SpawnMany(n)
- Pool: + Total (instances created)
- EnemySpawner obj: removed missing script, + DebugHud (spawner ref)
- saved scene, tested F→300 alive ✓
- commit+push

## M6 — Menu, pause, game over, high score
- script: SaveSystem.cs (Data{bestTime,bestKills}, JSON at persistentDataPath/save.json)
- script: GameManager.cs rewritten — State enum, static I, Elapsed, Kills; Set() toggles panels + timeScale; Esc pause (ignored while level-up has ts=0); Play/Resume/Retry/Restart; GameOver saves best, guarded vs double call
- script: Hud.cs (hp/xp fill, timer, kills+lv, weapon list)
- PlayerHealth: + Hp prop, Die → GameManager.GameOver
- PlayerLevel: + Progress prop
- Enemy: death → GameManager.I.Kills++
- scene UI (editor script): HUD (HpBar, XpBar, Timer, Kills, Weapons), MenuPanel (title, best, PLAY, hint), PausePanel (RESUME, QUIT TO MENU), GameOverPanel (result, RETRY, MENU)
- obj GameManager: refs wired, buttons → Play/Resume/Restart/Retry/Restart
- one scene, panels only; Restart = reload scene; Retry uses static skipMenu
- saved scene
- tested: menu→play→die→gameover+save.json→retry (Playing)→quit (Menu shows best) ✓
- commit+push: M6
- bug: editor in-memory Player.xpMult was 0 (leftover from edit-mode test cmd, scene file had 1) → reset to 1, saved
- PlayerLevel: + XpText "xp/next"
- Hud: kills line now "kills N   lv N   xp 5/17"
- commit+push
- ROADMAP: removed hit-stop from M7

## M7 — Art/audio/feel, build, publish
- SaveSystem: file IO → PlayerPrefs (WebGL-safe), try/catch
- ProjectSettings: companyName → FaraDuMatin
- built WebGL (Builds/WebGL, compression off) → tested in browser ✓; UI tiny → CanvasScaler: ScaleWithScreenSize 960x600 match 0.5, saved scene
- CameraFollow: SmoothDamp + static Shake(amount), decay
- script: Flash.cs (SpriteRenderer color flash, restores on disable)
- script: Fx.cs (static Damage/Death/Pickup → pooled prefabs)
- script: DamageNumber.cs (TextMesh, rises + fades, pool release)
- Enemy: flash + Fx.Damage on hit, Fx.Death(color) on kill
- PlayerHealth: flash red + shake on damage
- XpGem: Fx.Pickup
- prefabs (editor script): DamageNumber (TextMesh), DeathBurst, PickupBurst (ParticleSystem + AutoRelease), Particle.mat
- Enemy prefab + Player: + Flash; scene: + Fx obj wired
- Player moveSpeed 5→12; XpGem magnetSpeed 8→14 (must outrun player)
- saved scene
- CameraFollow smooth 0.08→0 (felt laggy at speed 12), saved scene
- CameraFollow smooth → 0.03, saved scene
- generate_audio: fal provider not configured (needs key) → skipped
- script: Sfx.cs (static Play(clip, vol, minInterval), 12 AudioSources, pitch var, per-clip rate cap)
- hooks: Enemy hit/death, PlayerHealth hurt (0.3s cap), XpGem pickup, PlayerLevel levelUp, GameManager click/gameOver
- scene: + Sfx obj (clips empty); folder Assets/Audio
- saved scene
- Tools/gen_sfx.py: python synth (square/sine/noise + envelope) → Assets/Audio/*.wav (7 clips)
- Sfx obj: all 7 clips wired, saved scene
- Assets/Audio/music.mp3 (user), import loadType Streaming
- Sfx: + music clip, musicVol 0.4, looping AudioSource on Awake
- Sfx obj: music wired, saved scene
- camera ortho 5→7 (shot range 8 was off-screen vertically), spawner radius 12→15, saved scene
- WeaponData: + fireSfx; Weapon: plays on successful Fire; gen shoot.wav/zap.wav; wired BasicShot+Boomerang=shoot, Lightning=zap
- user: camera ortho → 10
- UpgradeManager pool: removed Upgrade_Area; deleted asset (+meta); pool 14
- spawner radius 15→20 (ortho 10 = 16 half-width), saved scene
