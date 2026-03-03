# RealPlay-Project

We want to create a game that frees players from all that ad-slop and give you what was promised in those ads — without the bait-and-switch. RealPlay is a game that inspered from ads give you different games from what they are and make a game library from them you to enjoy.


## INCLUDED GAMES


## 1.  The Last Outpost 

- On this one you make walls and fire up the campfire to protect yourself from outside dangers and avoiding to die of cold. With basic gameplay it serves a fun and simple gameplay experience. Doesn't plan to make it a long term game but a game that opens up the road for us and gives the information of what we are promising to the player.

|          Mechanic        |                                  Detail                               |
|--------------------------|-----------------------------------------------------------------------|
|         **Move**         | Basically the only gameplay option you have, as i said simple but fun |
| **Collecting resources** | Cut down the trees and collect wood for various uses                  |
|       **Defence**        | Defence against to the polar bears with your manpower, walls, etc.    |
|       **Serving**        | Give the meat you collected to the customers for money                |
|       **Levels**         | "Yet to be implamented"                                               |
|      **Game Over**       | If you are unable to satisfy your customers or unable to defence      |


## 2. Ball Blast 

- Aim and shoot balls to destroy descending blocks before they reach the danger line. Each block has HP, and every 5 rows cleared increases the level and block toughness. Score points for each block destroyed and try to beat your high score!***

|      Mechanic       |                          Detail                          |
|---------------------|----------------------------------------------------------|
|        **Aim**      | Move mouse to aim the cannon                             |
|      **Shoot**      | Release left mouse button to fire                        |
|      **Blocks**     | Each block shows its remaining HP                        |
|     **Scoring**     | 10 pts per block destroyed                               |
|     **Levels**      | Every 5 rows cleared = +1 level (blocks get tougher)     |
|    **Game Over**    | Any block reaching the danger line ends the game         |




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
|
├── Packages/
│   └── manifest.json           # Unity package dependencies
|
├── ProjectSettings/            # Unity project configuration
|       ├── EditorBuildSettings.asset
|       ├── Physics2DSettings.asset 
|       ├── ProjectSettings.asset
|       └── TagManager.asset     
|              
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


## Geliştirme Ortamı

- **IDE**: Visual Studio Code
- **Game Engine**: Unity 6.3 LTS
- **VCS**: Git + GitHub (Private repo, v1.0'da public olacak)
- **Kısıtlamalar**: 32GB KYK interneti (sadece GitHub + API call, büyük dosya yok)

## Contributing
This project is developed by two first-year Computer Engineering students. Contributions, suggestions, and bug reports are welcome via GitHub Issues or Pull Requests.
