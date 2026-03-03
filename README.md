# RealPlay-Project

We want to create a game that frees players from all that ad-slop and give you what was promised in those ads — without the bait-and-switch. RealPlay is a **Ball Blast** style 2D arcade game built in Unity where you actually play the game shown in the advertisement.

## Game Overview

Numbered blocks descend toward your cannon row by row. Aim with your mouse, fire a volley of balls, and destroy every block before it reaches you. Each hit reduces a block's HP by 1; when it hits 0 the block explodes and you earn points. Survive as long as possible and reach higher levels for bigger blocks and more points.

| Mechanic | Detail |
|---|---|
| **Aim** | Move mouse to aim the cannon |
| **Shoot** | Release left mouse button to fire |
| **Blocks** | Each block shows its remaining HP |
| **Scoring** | 10 pts per block destroyed |
| **Levels** | Every 5 rows cleared = +1 level (blocks get tougher) |
| **Game Over** | Any block reaching the danger line ends the game |

## Project Structure

```
RealPlay-Project/
├── Assets/
│   ├── Scenes/          # Unity scene files
│   └── Scripts/
│       ├── GameManager.cs      # Game state, score & level
│       ├── PlayerController.cs # Cannon aim & fire
│       ├── BallController.cs   # Ball physics & block collision
│       ├── BlockController.cs  # Block HP display & destruction
│       ├── BlockSpawner.cs     # Row spawning & difficulty scaling
│       └── UIManager.cs        # HUD & game-over screen
├── Packages/
│   └── manifest.json    # Unity package dependencies
├── ProjectSettings/     # Unity project configuration
├── .gitignore
└── README.md
```

## Setup Instructions

1. **Install Unity 2022.3 LTS** from [Unity Hub](https://unity.com/download).
2. **Open the project** — in Unity Hub click *Open* and select the `RealPlay-Project` folder.
3. **Open the scene** — double-click `Assets/Scenes/MainScene.unity`.
4. **Scene hierarchy** you will need to create in the Editor:
   - `Main Camera` — Camera component, orthographic size 5.
   - `GameManager` — attach `GameManager.cs`.
   - `BlockSpawner` — attach `BlockSpawner.cs`; assign the Block prefab.
   - `Player` — Sprite (cannon graphic); attach `PlayerController.cs`; create a child `FirePoint` transform at the cannon tip.
   - `Canvas` — Screen Space Overlay; add Score/Level TextMeshPro labels and a Game Over panel with a Restart button; attach `UIManager.cs` and wire all references.
   - **Block Prefab** — Sprite + `BlockController.cs` + `TextMeshPro` child for the HP label + `Rigidbody2D` (kinematic) + `BoxCollider2D`.
   - **Ball Prefab** — Circle Sprite + `BallController.cs` + `Rigidbody2D` + `CircleCollider2D` + a bouncy `Physics Material 2D` (Bounciness = 1, Friction = 0).
5. **Press Play** — aim with your mouse and click to shoot!

## Contributing

This project is developed by two first-year Computer Engineering students. Contributions, suggestions, and bug reports are welcome via GitHub Issues or Pull Requests.
