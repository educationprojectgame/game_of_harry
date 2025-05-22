using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float dashSpeed = 7f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 2f;
    public float closeRange = 5f;
    public float farRange = 10f;
    public float decisionInterval = 1.5f;
    public float idleMoveChance = 0.2f;
    public float rotationSpeed = 5f;
    public float flipThreshold = 0.1f;

    public GameObject player;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canDash = true;
    private float lastDecisionTime;
    private bool isIdleMoving = false;
    private Vector2 idleMoveDirection = Vector2.right;
    private SpriteRenderer spriteRenderer;

    public Vector2 roomBoundsMin;
    public Vector2 roomBoundsMax;
    public float edgeAvoidanceDistance = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on this GameObject!");
            enabled = false;
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player GameObject not assigned in the Inspector!");
            enabled = false;
            return;
        }
         spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject!");
        }
        lastDecisionTime = Time.time;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        MakeMovementDecision();

        ClampPositionWithinBounds();

        float maxSpeed = 5f;
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void MakeMovementDecision()
    {
        if (Time.time - lastDecisionTime >= decisionInterval)
        {
            lastDecisionTime = Time.time;

            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= closeRange)
            {
                isIdleMoving = false;
                ChaseWithOccasionalDash();
                FlipTowardsPlayer();
            }
            else if (distanceToPlayer > farRange)
            {
                if (!isIdleMoving && Random.value < idleMoveChance)
                {
                    isIdleMoving = true;
                    Vector2 newDirection = Random.insideUnitCircle.normalized;
                    StartCoroutine(SmoothRotation(newDirection));
                }
                RandomMoveAround();
                FlipTowardsPlayer();
            }
            else
            {
                isIdleMoving = false;
                if (Random.value < 0.5f)
                {
                    ApproachPlayer();
                    FlipTowardsPlayer();
                }
                else
                {
                    FleeFromPlayer();
                    FlipTowardsPlayer();
                }
            }
        }
        if (isIdleMoving)
        {
            rb.AddForce(idleMoveDirection * moveSpeed, ForceMode2D.Force);
            FlipTowardsPlayer();
        }
    }

    void ChaseWithOccasionalDash()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        Vector2 targetVelocity = direction * moveSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, 0.1f);

        if (canDash && Random.value < 0.3f)
        {
            StartCoroutine(Dash(direction));
        }
    }

    void RandomMoveAround()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.AddForce(randomDirection * moveSpeed, ForceMode2D.Force);

        if (canDash && Random.value < 0.2f)
        {
            StartCoroutine(Dash(randomDirection));
        }
    }

    void ApproachPlayer()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        Vector2 targetVelocity = direction * moveSpeed * 0.7f;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, 0.1f);
    }

    void FleeFromPlayer()
    {
        Vector2 direction = ((Vector2)transform.position - (Vector2)player.transform.position).normalized;
        Vector2 targetVelocity = direction * moveSpeed * 0.8f;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, 0.1f);
    }

    IEnumerator Dash(Vector2 direction)
    {
        if (!canDash) yield break;

        canDash = false;
        isDashing = true;

        Vector2 dashForce = direction * dashSpeed;
        rb.AddForce(dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void ClampPositionWithinBounds()
    {
        float x = Mathf.Clamp(transform.position.x, roomBoundsMin.x + edgeAvoidanceDistance, roomBoundsMax.x - edgeAvoidanceDistance);
        float y = Mathf.Clamp(transform.position.y, roomBoundsMin.y + edgeAvoidanceDistance, roomBoundsMax.y - edgeAvoidanceDistance);

        Vector2 desiredPosition = new Vector2(x, y);
        Vector2 forceDirection = (desiredPosition - (Vector2)transform.position).normalized;
        Vector2 currentVelocity = rb.linearVelocity;
        float wallAvoidanceForce = 5f;

        if (transform.position.x <= roomBoundsMin.x + edgeAvoidanceDistance && currentVelocity.x < 0)
        {
            rb.AddForce(Vector2.right * wallAvoidanceForce, ForceMode2D.Force);
        }
        if (transform.position.x >= roomBoundsMax.x - edgeAvoidanceDistance && currentVelocity.x > 0)
        {
            rb.AddForce(Vector2.left * wallAvoidanceForce, ForceMode2D.Force);
        }
        if (transform.position.y <= roomBoundsMin.y + edgeAvoidanceDistance && currentVelocity.y < 0)
        {
            rb.AddForce(Vector2.up * wallAvoidanceForce, ForceMode2D.Force);
        }
        if (transform.position.y >= roomBoundsMax.y - edgeAvoidanceDistance && currentVelocity.y > 0)
        {
            rb.AddForce(Vector2.down * wallAvoidanceForce, ForceMode2D.Force);
        }

        rb.AddForce(forceDirection * moveSpeed * 2f, ForceMode2D.Force);
    }

  IEnumerator SmoothRotation(Vector2 newDirection)
    {
        float startRotation = Mathf.Atan2(idleMoveDirection.y, idleMoveDirection.x) * Mathf.Rad2Deg;
        float endRotation = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed / Mathf.Abs(Mathf.DeltaAngle(startRotation, endRotation));
            float rotation = Mathf.LerpAngle(startRotation, endRotation, t);
            idleMoveDirection = new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad));
            yield return null;
        }

        idleMoveDirection = newDirection;
    }

    void FlipTowardsPlayer()
    {
        if (spriteRenderer == null || player == null) return;

        float directionX = player.transform.position.x - transform.position.x;

        if (Mathf.Abs(directionX) > flipThreshold)
        {
            spriteRenderer.flipX = directionX > 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube((roomBoundsMin + roomBoundsMax) * 0.5f, roomBoundsMax - roomBoundsMin);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, closeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, farRange);
    }
}