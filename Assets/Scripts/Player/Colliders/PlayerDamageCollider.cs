using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCollider : MonoBehaviour
{
    private Player player;
    public LayerMask enemyLayer;
    public int contactDamage = 1;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            Debug.Log("É um inimigo! Causando dano.");
            Vector2 knockbackDirection = (player.transform.position - collision.transform.position).normalized;

            player.TakeDamage(contactDamage);
            player.StartCoroutine(player.ApplyKnockback(knockbackDirection));
        }
    }
}
