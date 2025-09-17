using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.isGrounded = false;
    }
}
