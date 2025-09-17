using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();

        if (player == null)
        {
            Debug.LogError("Player component not found in parent hierarchy!", this);
            // Desabilita o script se o player não for encontrado
            enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null && collision != null)
        {
            player.isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player != null && collision != null)
        {
            player.isGrounded = false;
        }
    }
}
