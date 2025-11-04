using UnityEngine;

public class EnemySceneCheck : MonoBehaviour
{
    private Enemy enemy;

    public LayerMask groundMask;
    public LayerMask wallMask;

    public float wallCheckDistance = 1f;
    public float groundCheckDistance = 2f;

    public float groundCheckHorizontalOffset = 0.5f;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void FixedUpdate()
    {
        if (enemy == null) return;

        Vector2 facingDirection = enemy.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        Vector3 groundCheckOrigin = transform.position + (Vector3)(facingDirection * groundCheckHorizontalOffset);

        RaycastHit2D groundCheck = Physics2D.Raycast(groundCheckOrigin, Vector2.down, groundCheckDistance, groundMask);
        enemy.isGrounded = groundCheck;

        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, facingDirection, wallCheckDistance, wallMask);
        enemy.isWalled = wallCheck;
    }

    private void OnDrawGizmos()
    {
        if (enemy == null)
        {
            if (GetComponentInParent<Enemy>() != null)
                enemy = GetComponentInParent<Enemy>();
            else
                return; 
        }

        Vector2 facingDirection = enemy.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector3 groundCheckOrigin = transform.position + (Vector3)(facingDirection * groundCheckHorizontalOffset);

        Gizmos.color = enemy.isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(groundCheckOrigin, groundCheckOrigin + (Vector3.down * groundCheckDistance));

        Gizmos.color = enemy.isWalled ? Color.blue : Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(facingDirection * wallCheckDistance));
    }
}