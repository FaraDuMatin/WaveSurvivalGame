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
