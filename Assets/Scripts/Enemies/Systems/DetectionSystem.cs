using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionSystem
{
    private Enemy enemy; 
    private Transform player;

    public DetectionSystem(Enemy enemy)
    {
        this.enemy = enemy;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public bool SeesPlayer()
    {
        if (!player) return false;

        bool playerIsInFront = enemy.facingRight
            ? player.position.x > enemy.transform.position.x
            : player.position.x < enemy.transform.position.x;

        if (!playerIsInFront) return false;

        Vector2 direction = Vector2.right * Mathf.Sign(enemy.transform.localScale.x);

        // Desenha o raycast no editor (Scene View)
        Debug.DrawRay(enemy.transform.position, direction * enemy.viewDistance, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction, enemy.viewDistance, enemy.detectionMask);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }

}


