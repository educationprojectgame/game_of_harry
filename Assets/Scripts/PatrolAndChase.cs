using UnityEngine;

public class PatrolAndChase : MonoBehaviour
{
    public float speed = 4f;
    public float chaseSpeedMultiplier = 1.5f;
    public float detectionDistance = 10f;
    public Transform[] patrolPoints;
    public float closeEnoughDistance = 0.1f;
    public Transform player;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            Debug.LogError("Player Transform not assigned! Please drag the Player GameObject to the Player field in the Inspector.");
            enabled = false;
            return;
        }
        if (patrolPoints == null || patrolPoints.Length < 4)
        {
            Debug.LogError("Not enough patrol points! Need at least 4.");
            enabled = false;
            return;
        }
        SetMoveDirection();
    }

    void FixedUpdate()
    {
        if (player == null) return;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
        if (isChasing)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Vector2 targetPosition = patrolPoints[currentPatrolIndex].position;
        if (Vector2.Distance(transform.position, targetPosition) < closeEnoughDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
        SetMoveDirection();
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    void Chase()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * chaseSpeedMultiplier * Time.fixedDeltaTime);
    }

     void SetMoveDirection()
    {
        Vector2 targetPosition = patrolPoints[currentPatrolIndex].position;
        moveDirection = ((Vector2)targetPosition - (Vector2)transform.position).normalized;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
        if (patrolPoints != null)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                if (patrolPoints[i] != null)
                {
                    Gizmos.DrawSphere(patrolPoints[i].position, 0.2f);
                    if (i < patrolPoints.Length - 1 && patrolPoints[i + 1] != null)
                    {
                        Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                    } else if (i == patrolPoints.Length -1 && patrolPoints.Length > 0 && patrolPoints[0] != null){
                         Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
                    }
                }
            }
        }
    }
}