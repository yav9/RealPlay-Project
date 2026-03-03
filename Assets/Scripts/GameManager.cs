using UnityEngine;

/// <summary>
/// Singleton that manages overall game state, score, and level progression.
/// Attach to an empty GameObject named "GameManager" in the scene.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Playing, GameOver }

    public GameState CurrentState { get; private set; } = GameState.Playing;

    [Header("Level Settings")]
    [Tooltip("How much the maximum block HP increases each level")]
    [SerializeField] private int hpIncreasePerLevel = 2;

    private int _score;
    private int _level = 1;

    // ── Events ──────────────────────────────────────────────────────────────
    public event System.Action<int> OnScoreChanged;
    public event System.Action<int> OnLevelChanged;
    public event System.Action OnGameOver;

    // ── Properties ──────────────────────────────────────────────────────────
    public int Score => _score;
    public int Level => _level;

    /// <summary>Maximum HP a block can start with at the current level.</summary>
    public int MaxBlockHP => _level * hpIncreasePerLevel + hpIncreasePerLevel;

    // ── Unity lifecycle ──────────────────────────────────────────────────────
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // ── Public API ───────────────────────────────────────────────────────────

    /// <summary>Add points and fire the score-changed event.</summary>
    public void AddScore(int points)
    {
        if (CurrentState != GameState.Playing)
            return;

        _score += points;
        OnScoreChanged?.Invoke(_score);
    }

    /// <summary>Advance the level counter and fire the level-changed event.</summary>
    public void AdvanceLevel()
    {
        if (CurrentState != GameState.Playing)
            return;

        _level++;
        OnLevelChanged?.Invoke(_level);
    }

    /// <summary>Transition to GameOver state and notify listeners.</summary>
    public void TriggerGameOver()
    {
        if (CurrentState == GameState.GameOver)
            return;

        CurrentState = GameState.GameOver;
        OnGameOver?.Invoke();
    }

    /// <summary>Reload the active scene to restart the game.</summary>
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
