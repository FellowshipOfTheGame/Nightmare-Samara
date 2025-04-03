using UnityEngine;

public class Detection : MonoBehaviour
{
    [SerializeField] private float viewDistance = 5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;

    private Transform player;
    private bool facingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public bool SeesPlayer()
    {
        if (!player) return false;

        float dirToPlayer = player.position.x - transform.position.x;

        // Verifica se o jogador está na direção correta e dentro do alcance
        if ((facingRight && dirToPlayer < 0) || (!facingRight && dirToPlayer > 0) || Mathf.Abs(dirToPlayer) > viewDistance)
            return false;

        // Verifica se há obstáculos bloqueando a visão
        return !Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(dirToPlayer), viewDistance, obstacleLayer);
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (facingRight ? Vector3.right : Vector3.left) * viewDistance);
    }

    public bool isFacingRight()
    {
        return facingRight;
    }
}
