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
