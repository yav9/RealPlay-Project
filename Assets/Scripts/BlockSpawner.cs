using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns rows of numbered blocks and advances them toward the player.
/// Triggers a game-over if any block crosses the danger line.
/// Also advances the level after every N rows are cleared.
/// </summary>
public class BlockSpawner : MonoBehaviour
{
    [Header("Grid layout")]
    [Tooltip("Block prefab that has a BlockController and a SpriteRenderer")]
    [SerializeField] private GameObject blockPrefab;

    [Tooltip("Number of columns in each row")]
    [SerializeField] private int columns = 5;

    [Tooltip("Horizontal spacing between block centres")]
    [SerializeField] private float columnSpacing = 1.2f;

    [Tooltip("Vertical spacing between rows")]
    [SerializeField] private float rowSpacing = 1.2f;

    [Tooltip("Y position where new rows first appear")]
    [SerializeField] private float spawnY = 4.5f;

    [Tooltip("Y position that triggers Game Over if any block reaches it")]
    [SerializeField] private float dangerY = -3.5f;

    [Header("Timing")]
    [Tooltip("Seconds between each row advance / new spawn")]
    [SerializeField] private float advanceInterval = 3f;

    [Header("Difficulty")]
    [Tooltip("Rows cleared before the level advances")]
    [SerializeField] private int rowsPerLevel = 5;

    // ── State ────────────────────────────────────────────────────────────────
    private readonly List<GameObject> _activeBlocks = new();
    private float _timer;
    private int _rowsSpawned;

    // ── Unity lifecycle ──────────────────────────────────────────────────────
    private void Start()
    {
        SpawnRow();
    }

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        _timer += Time.deltaTime;
        if (_timer >= advanceInterval)
        {
            _timer = 0f;
            AdvanceBlocks();
            SpawnRow();
            CheckDanger();
        }
    }

    // ── Private helpers ──────────────────────────────────────────────────────

    private void SpawnRow()
    {
        _rowsSpawned++;
        int maxHP = GameManager.Instance != null ? GameManager.Instance.MaxBlockHP : 2;

        float totalWidth = (columns - 1) * columnSpacing;
        float startX = -totalWidth / 2f;

        for (int col = 0; col < columns; col++)
        {
            // Randomly skip ~25 % of cells for variety
            if (Random.value < 0.25f)
                continue;

            Vector3 pos = new Vector3(startX + col * columnSpacing, spawnY, 0f);
            GameObject block = Instantiate(blockPrefab, pos, Quaternion.identity);
            block.GetComponent<BlockController>()?.SetHP(Random.Range(1, maxHP + 1));
            _activeBlocks.Add(block);
        }

        // Level up every rowsPerLevel rows
        if (_rowsSpawned % rowsPerLevel == 0)
            GameManager.Instance?.AdvanceLevel();
    }

    private void AdvanceBlocks()
    {
        // Remove destroyed entries, then move survivors down by one row
        _activeBlocks.RemoveAll(b => b == null);
        foreach (GameObject block in _activeBlocks)
            block.transform.position += Vector3.down * rowSpacing;
    }

    private void CheckDanger()
    {
        _activeBlocks.RemoveAll(b => b == null);
        foreach (GameObject block in _activeBlocks)
        {
            if (block.transform.position.y <= dangerY)
            {
                GameManager.Instance?.TriggerGameOver();
                return;
            }
        }
    }
}
