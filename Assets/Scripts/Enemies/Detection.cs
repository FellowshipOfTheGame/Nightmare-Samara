using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour
{
    [SerializeField] private float viewDistance = 4f;
    [SerializeField] private float loseTime = 4f;
    [SerializeField] private LayerMask detectionMask;

    private Transform player;
    private bool facingRight = true;

    private bool losing = false;
    private bool lostPlayer = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public bool SeesPlayer()
    {
        if (!player) return false;

        bool playerIsInFront = facingRight
        ? player.position.x > transform.position.x
        : player.position.x < transform.position.x;

        if (!playerIsInFront)
            return false;

        Vector2 direction = facingRight ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewDistance, detectionMask);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = SeesPlayer() ? Color.red : Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (facingRight ? Vector3.right : Vector3.left) * viewDistance);
    }


    public bool LosePlayer()
    {
        if (!player) return false;

        // Se já perdeu o jogador, retorna true uma única vez
        if (lostPlayer)
        {
            lostPlayer = false; // reseta
            return true;
        }

        // Se saiu da câmera e ainda não está esperando
        if (!IsVisibleInCamera() && !losing)
        {
            StartCoroutine(WaitToLose());
        }

        return false;
    }

    bool IsVisibleInCamera()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewPos.x >= 0 && viewPos.x <= 1 &&
           viewPos.y >= 0 && viewPos.y <= 1 &&
           viewPos.z > 0;
    }

    private IEnumerator WaitToLose()
    {
        losing = true;
        yield return new WaitForSeconds(loseTime);
        lostPlayer = true;
        losing = false;
        Debug.Log("Tempo passou, perdeu o jogador.");
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public bool isFacingRight() => facingRight;

    public Transform PlayerPosition() => player;
   
}
