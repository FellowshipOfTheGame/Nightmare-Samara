using UnityEngine;
using System.Collections;

public class PatrolState : EnemyStates
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float waitTime = 2f;

    [Header("Collision Detection")]
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float edgeCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Vector2 startPosition;
    private bool isWaiting = false;
    private bool shouldFlip = false;
    private bool facingRight = true; // Cache the facing direction

    protected override void Awake()
    {
        base.Awake();
        if (detection != null)
        {
            facingRight = detection.isFacingRight();
        }
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (detection == null) return;

       
        if (detection.SeesPlayer())
        {
            enemy.ChangeState(enemy.chaseState);
            return;
        }
      
        if (!isWaiting)
        {
            CheckForObstacles();

            if (shouldFlip || ReachedPatrolDistance())
            {
                StartCoroutine(WaitBeforeFlip());
                return;
            }

            Move();
        }
    }

    void CheckForObstacles()
    {
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;

        // Wall detection
        RaycastHit2D wallHit = Physics2D.Raycast(
            transform.position,
            direction,
            wallCheckDistance,
            wallLayer);

        // Edge detection
        Vector2 edgeCheckPos = (Vector2)transform.position +
                             (direction * edgeCheckDistance) +
                             (Vector2.down * 0.5f);

        bool hasGround = Physics2D.Raycast(
            edgeCheckPos,
            Vector2.down,
            0.1f,
            groundLayer);

        shouldFlip = wallHit.collider != null || !hasGround;
    }

    bool ReachedPatrolDistance()
    {
        return Vector2.Distance(startPosition, transform.position) >= patrolDistance;
    }

    void Move()
    {
        float direction = facingRight ? speed : -speed;
        transform.Translate(Vector2.right * direction * Time.deltaTime);
    }

    IEnumerator WaitBeforeFlip()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        facingRight = !facingRight;
        if (detection != null) detection.Flip();
        startPosition = transform.position;
        shouldFlip = false;
        isWaiting = false;
    }

    private void OnDrawGizmosSelected()
    {
        // Use the cached facingRight instead of detection reference
        Vector2 wallDir = facingRight ? Vector2.right : Vector2.left;

        // Wall check visualization
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + wallDir * wallCheckDistance);

        // Edge check visualization
        Vector2 edgeStart = (Vector2)transform.position +
                          (wallDir * edgeCheckDistance) +
                          (Vector2.down * 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(edgeStart, edgeStart + Vector2.down * 0.1f);
    }
}