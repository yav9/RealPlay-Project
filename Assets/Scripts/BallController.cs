using UnityEngine;

/// <summary>
/// Controls a single ball's movement and collision with blocks.
/// Uses Rigidbody2D for physics-based bouncing.
/// The ball auto-destroys when it exits the bottom of the screen.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallController : MonoBehaviour
{
    [Tooltip("Speed at which the ball travels (units/second). Set programmatically via Launch().")]
    [SerializeField] private float speed = 12f;

    [Tooltip("Y-coordinate below which the ball is automatically destroyed")]
    [SerializeField] private float destroyBelowY = -10f;

    private Rigidbody2D _rb;
    private bool _launched;

    // ── Unity lifecycle ──────────────────────────────────────────────────────
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Keep the ball's material bouncy (set Bounciness = 1, Friction = 0
        // on a Physics Material 2D assigned to this collider in the Inspector).
    }

    private void Update()
    {
        if (transform.position.y < destroyBelowY)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect velocity so the ball bounces correctly
        Vector2 reflected = Vector2.Reflect(_rb.linearVelocity.normalized, collision.contacts[0].normal);
        _rb.linearVelocity = reflected * speed;

        // Damage the block (if we hit one)
        BlockController block = collision.gameObject.GetComponent<BlockController>();
        if (block != null)
            block.TakeDamage(1);
    }

    // ── Public API ───────────────────────────────────────────────────────────

    /// <summary>Give the ball an initial velocity and activate its movement.
    /// Calling this more than once has no effect.</summary>
    public void Launch(Vector2 velocity)
    {
        if (_launched)
            return;

        speed = velocity.magnitude;
        _rb.linearVelocity = velocity;
        _launched = true;
    }
}
