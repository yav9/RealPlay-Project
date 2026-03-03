using System.Collections;
using UnityEngine;

/// <summary>
/// Handles player input for aiming and firing balls from the cannon.
/// Attach to the cannon sprite/GameObject.
/// Wire up <see cref="ballPrefab"/> in the Inspector.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Cannon")]
    [Tooltip("Prefab that contains the BallController component")]
    [SerializeField] private GameObject ballPrefab;

    [Tooltip("Point from which balls are spawned (child transform at cannon tip)")]
    [SerializeField] private Transform firePoint;

    [Header("Firing")]
    [Tooltip("Number of balls fired per shot")]
    [SerializeField] private int ballCount = 1;

    [Tooltip("Delay in seconds between consecutive balls in the same volley")]
    [SerializeField] private float ballDelay = 0.08f;

    [Tooltip("Base speed given to each launched ball")]
    [SerializeField] private float ballSpeed = 12f;

    // Minimum angle from horizontal so the player can't shoot straight sideways
    private const float MinAimAngle = 10f;

    private bool _isFiring;
    private Camera _mainCamera;
    private LineRenderer _aimLine;

    // ── Unity lifecycle ──────────────────────────────────────────────────────
    private void Awake()
    {
        _mainCamera = Camera.main;
        _aimLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        if (_isFiring)
            return;

        HandleAiming();

        if (Input.GetMouseButtonUp(0))
            TryFire();
    }

    // ── Private helpers ──────────────────────────────────────────────────────

    private void HandleAiming()
    {
        Vector2 direction = GetAimDirection();
        RotateCannon(direction);
        DrawAimLine(direction);
    }

    /// <summary>Returns a normalised direction from the cannon toward the mouse cursor,
    /// clamped so the angle is never too close to horizontal.</summary>
    private Vector2 GetAimDirection()
    {
        Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 dir = ((Vector2)mouseWorld - (Vector2)firePoint.position).normalized;

        // Clamp angle above the minimum so balls always travel upward
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, MinAimAngle, 180f - MinAimAngle);
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    private void RotateCannon(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void DrawAimLine(Vector2 direction)
    {
        if (_aimLine == null)
            return;

        _aimLine.SetPosition(0, firePoint.position);
        _aimLine.SetPosition(1, (Vector2)firePoint.position + direction * 5f);
    }

    private void TryFire()
    {
        if (ballPrefab == null || firePoint == null)
        {
            Debug.LogWarning("PlayerController: ballPrefab or firePoint not assigned.");
            return;
        }

        Vector2 direction = GetAimDirection();
        StartCoroutine(FireVolley(direction));
    }

    private IEnumerator FireVolley(Vector2 direction)
    {
        _isFiring = true;

        for (int i = 0; i < ballCount; i++)
        {
            SpawnBall(direction);
            yield return new WaitForSeconds(ballDelay);
        }

        _isFiring = false;
    }

    private void SpawnBall(Vector2 direction)
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, Quaternion.identity);
        BallController bc = ball.GetComponent<BallController>();
        if (bc != null)
            bc.Launch(direction * ballSpeed);
    }

    // ── Public API ───────────────────────────────────────────────────────────

    /// <summary>Increase the ball count (called by power-up logic or level-up bonuses).</summary>
    public void AddBall(int amount = 1) => ballCount += amount;
}
