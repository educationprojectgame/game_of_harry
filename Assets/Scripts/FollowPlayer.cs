using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float activationDistance = 10f;
    public float chaseDistance = 2f;
    public float slowDownDistance = 5f;
    public float minSpeed = 1f;
    public float idleTimeMin = 1f;
    public float idleTimeMax = 3f;
    public float changeDirectionIntervalMin = 2f;
    public float changeDirectionIntervalMax = 5f;
    private Rigidbody2D rb;
    private bool isActivated = false;
    private Vector2 moveDirection;
    private Coroutine patrolCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D component not found on this GameObject!");
        moveDirection = Random.insideUnitCircle.normalized;
        patrolCoroutine = StartCoroutine(Patrol());
    }

    void FixedUpdate()
    {
        if (player != null && rb != null)
        {
            Vector2 targetPosition = player.position;
            Vector2 currentPosition = transform.position;
            float distanceToPlayer = Vector2.Distance(targetPosition, currentPosition);
            if (!isActivated && distanceToPlayer <= activationDistance)
            {
                isActivated = true;
                StopCoroutine(patrolCoroutine);
                rb.linearVelocity = Vector2.zero;
            }
            if (isActivated)
            {
                if (distanceToPlayer > chaseDistance)
                {
                    float currentSpeed = speed;
                    if (distanceToPlayer < slowDownDistance)
                    {
                        float slowDownFactor = Mathf.InverseLerp(chaseDistance, slowDownDistance, distanceToPlayer);
                        currentSpeed = Mathf.Lerp(minSpeed, speed, slowDownFactor);
                    }
                    Vector2 direction = (targetPosition - currentPosition).normalized;
                    Vector2 newPosition = currentPosition + direction * currentSpeed * Time.fixedDeltaTime;
                    rb.MovePosition(newPosition);
                }
                else rb.linearVelocity = Vector2.zero;
            }
            else rb.linearVelocity = moveDirection * speed;
        }
        else if (player == null) Debug.LogWarning("Player not assigned to FollowPlayer script!");
        else Debug.LogError("Rigidbody2D component not found on this GameObject!");
    }

    private IEnumerator Patrol()
    {
        while (!isActivated)
        {
            float changeDirInterval = Random.Range(changeDirectionIntervalMin, changeDirectionIntervalMax);
            yield return new WaitForSeconds(changeDirInterval);
            rb.linearVelocity = Vector2.zero;
            float idleTime = Random.Range(idleTimeMin, idleTimeMax);
            yield return new WaitForSeconds(idleTime);
            moveDirection = Random.insideUnitCircle.normalized;
        }
    }
}