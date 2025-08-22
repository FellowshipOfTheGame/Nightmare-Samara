using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isWalled;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            Debug.Log("GROUNDED");
        isGrounded = true;
        if (collision.CompareTag("Wall"))
            isWalled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            isGrounded = false;
        if (collision.CompareTag("Wall"))
            isWalled = false;
    }
}
