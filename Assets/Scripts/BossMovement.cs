using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float sprintSpeedMultiplier = 1.5f;
    public float accelerationRate = 1f;
    public float decelerationRate = 1f;
    public float dashDistance = 7f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 3f;
    public float playerProximityRange = 10f;
    public LayerMask projectileLayer;
    public float projectileDetectionRadius = 5f;
    public float changeDirectionInterval = 3f;
    public Vector2 roomBoundsMin = new Vector2(-10f, -10f);
    public Vector2 roomBoundsMax = new Vector2(10f, 10f);
    public GameObject player;
    private Rigidbody2D rb;
    private Vector2 currentMoveDirection;
    private float currentSpeed;
    private bool isDashing = false;
    private float lastDashTime;
    private SpriteRenderer spriteRenderer;
    private Coroutine speedChangeCoroutine;
    private bool dodgeRight = true;
    private float lastProjectileCheckTime;
    public float projectileCheckInterval = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("BossMovement requires a Rigidbody2D component.");
            enabled = false;
            return;
        }
        rb.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer not found. Flipping will not work.");
        }
        if (player == null)
        {
            Debug.LogError("Player GameObject not assigned!  Assign it in the Inspector.");
            enabled = false;
            return;
        }
        currentSpeed = baseSpeed;
        lastDashTime = -dashCooldown;
        StartCoroutine(ChangeDirectionRandomly());
        lastProjectileCheckTime = -projectileCheckInterval;
    }

    void Update()
    {
        if (isDashing) return;
        Move();
        if (Time.time - lastDashTime >= dashCooldown && CheckPlayerProximity())
        {
            StartCoroutine(Dash(GetDirectionToPlayer()));
        }
        if (Time.time - lastProjectileCheckTime >= projectileCheckInterval)
        {
            lastProjectileCheckTime = Time.time;
            CheckForProjectiles();
        }
        Flip();
    }

    void Move()
    {
        Vector2 targetVelocity = currentMoveDirection * currentSpeed;
        if (speedChangeCoroutine == null)
        {
             rb.linearVelocity = targetVelocity;
        }
    }

    bool CheckPlayerProximity()
    {
        if (player == null) return false;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        return distanceToPlayer <= playerProximityRange;
    }

    Vector2 GetDirectionToPlayer()
    {
        if (player == null) return currentMoveDirection;
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        return direction;
    }

     IEnumerator ChangeDirectionRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionInterval);
            currentMoveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }

    IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;
        lastDashTime = Time.time;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + direction * dashDistance;
        targetPosition.x = Mathf.Clamp(targetPosition.x, roomBoundsMin.x, roomBoundsMax.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, roomBoundsMin.y, roomBoundsMax.y);
        float timeElapsed = 0;
        while (timeElapsed < dashDuration)
        {
            float t = timeElapsed / dashDuration;
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, t));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        rb.MovePosition(targetPosition);
        isDashing = false;
    }

    void CheckForProjectiles()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, projectileDetectionRadius, projectileLayer);

        if (hits.Length > 0)
        {
               Vector2 dodgeDirection = GetDodgeDirection();
               StartCoroutine(Dash(dodgeDirection));
        }
    }

    Vector2 GetDodgeDirection()
    {
        Vector2 directionToPlayer = GetDirectionToPlayer();
        Vector2 dodgeDirection;
        if (dodgeRight)
        {
            dodgeDirection = new Vector2(directionToPlayer.y, -directionToPlayer.x).normalized;
        }
        else
        {
            dodgeDirection = new Vector2(-directionToPlayer.y, directionToPlayer.x).normalized;
        }
        dodgeRight = !dodgeRight;
        return dodgeDirection;
    }

    IEnumerator DodgeProjectile()
    {
          Vector2 dodgeDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
          yield return StartCoroutine(Dash(dodgeDirection));
    }

    void Flip()
    {
        if (spriteRenderer == null) return;
        if (player == null) return;
        if (transform.position.x < player.transform.position.x && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if (transform.position.x > player.transform.position.x && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerProximityRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, projectileDetectionRadius);
        Gizmos.color = Color.green;
        Vector3 minBounds = new Vector3(roomBoundsMin.x, roomBoundsMin.y, 0);
        Vector3 maxBounds = new Vector3(roomBoundsMax.x, roomBoundsMax.y, 0);
        Gizmos.DrawWireCube((minBounds + maxBounds) / 2, maxBounds - minBounds);
    }
}