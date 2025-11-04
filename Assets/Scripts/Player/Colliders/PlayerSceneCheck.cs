using UnityEngine;

public class PlayerSceneCheck : MonoBehaviour
{
    private Player player;
    public Vector2 groundCheckSize;
    public float wallCheckSize = 1f;
    public LayerMask groundMask;
    public LayerMask wallMask;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        RaycastHit2D groundCheck = Physics2D.BoxCast(transform.position, groundCheckSize, 0f, Vector2.down, 1f, groundMask);
        player.isGrounded = groundCheck; 

        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position, player.transform.localScale.x > 0 ? Vector2.right : Vector2.left, wallCheckSize, wallMask);
        player.isWalled = wallCheck;
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = player.isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * 0.01f, groundCheckSize);

        Gizmos.color = player.isWalled ? Color.blue : Color.yellow;
        Vector2 dir = player.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)dir * wallCheckSize);

    }

}