using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages all in-game UI: score display, level display, and game-over panel.
/// Wire all references up in the Inspector.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Game Over Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button restartButton;

    // ── Unity lifecycle ──────────────────────────────────────────────────────
    private void Start()
    {
        // Hide game-over panel at startup
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Subscribe to GameManager events
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged += UpdateScore;
            GameManager.Instance.OnLevelChanged += UpdateLevel;
            GameManager.Instance.OnGameOver += ShowGameOver;
        }

        // Initialise display
        UpdateScore(GameManager.Instance != null ? GameManager.Instance.Score : 0);
        UpdateLevel(GameManager.Instance != null ? GameManager.Instance.Level : 1);

        // Hook restart button
        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestartClicked);
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks / stale references after scene reload
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChanged -= UpdateScore;
            GameManager.Instance.OnLevelChanged -= UpdateLevel;
            GameManager.Instance.OnGameOver -= ShowGameOver;
        }
    }

    // ── Event handlers ───────────────────────────────────────────────────────

    private void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    private void UpdateLevel(int level)
    {
        if (levelText != null)
            levelText.text = $"Level: {level}";
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (finalScoreText != null && GameManager.Instance != null)
            finalScoreText.text = $"Final Score\n{GameManager.Instance.Score}";
    }

    private void OnRestartClicked()
    {
        GameManager.Instance?.RestartGame();
    }
}
