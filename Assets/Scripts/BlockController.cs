using TMPro;
using UnityEngine;

/// <summary>
/// Represents a single numbered block.
/// Shows the remaining HP as a text label.
/// Destroys itself when HP reaches zero and awards points.
/// </summary>
public class BlockController : MonoBehaviour
{
    [Header("HP")]
    [Tooltip("Initial hit-points; set by BlockSpawner at runtime")]
    [SerializeField] private int hp = 1;

    [Header("References")]
    [Tooltip("TextMeshPro label that displays the current HP")]
    [SerializeField] private TextMeshPro hpLabel;

    [Tooltip("Points awarded to the player when this block is destroyed")]
    [SerializeField] private int pointValue = 10;

    // ── Unity lifecycle ──────────────────────────────────────────────────────
    private void Start()
    {
        RefreshLabel();
    }

    // ── Public API ───────────────────────────────────────────────────────────

    /// <summary>Called by <see cref="BallController"/> on every hit.</summary>
    public void TakeDamage(int amount)
    {
        hp -= amount;
        RefreshLabel();

        if (hp <= 0)
            DestroyBlock();
    }

    /// <summary>Assign HP from outside (used by BlockSpawner).</summary>
    public void SetHP(int value)
    {
        hp = Mathf.Max(1, value);
        RefreshLabel();
    }

    // ── Private helpers ──────────────────────────────────────────────────────

    private void RefreshLabel()
    {
        if (hpLabel != null)
            hpLabel.text = hp.ToString();
    }

    private void DestroyBlock()
    {
        GameManager.Instance?.AddScore(pointValue);
        Destroy(gameObject);
    }
}
