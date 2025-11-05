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

    public bool CheckForObstacles()
    {
        if (enemy == null) return true;

        Vector2 facingDirection = enemy.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        Vector3 groundCheckOrigin = transform.position + (Vector3)(facingDirection * groundCheckHorizontalOffset);

        RaycastHit2D groundCheck = Physics2D.Raycast(groundCheckOrigin, Vector2.down, groundCheckDistance, groundMask);

        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, facingDirection, wallCheckDistance, wallMask);

        return !groundCheck || wallCheck;
    }

}