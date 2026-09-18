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
