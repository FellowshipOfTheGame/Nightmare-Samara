using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection
{
    private Enemy enemy; 
    private Transform player;

    private bool losing = false;
    private bool lostPlayer = false;

    public Detection(Enemy enemy)
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

        Vector2 direction = enemy.facingRight ? Vector2.right : Vector2.left;

        // Desenha o raycast no editor (Scene View)
        Debug.DrawRay(enemy.transform.position, direction * enemy.viewDistance, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, direction, enemy.viewDistance, enemy.detectionMask);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }



    public bool HasLostPlayer()
    {
        if (!player) return false;

        // Se j� perdeu o jogador, retorna true uma �nica vez
        // Retorna o valor de lostPlayer e reseta a flag para evitar que seja chamada m�ltiplas vezes
        if (lostPlayer)
        {
            lostPlayer = false; // reseta
            lostPlayer = false;
            return true;
        }

        // Se saiu da c�mera e ainda n�o est� esperando
        if (!IsVisibleInCamera() && !losing)
        {
            enemy.StartCoroutine(WaitToLose());
        }

        return false;
    }

    private IEnumerator WaitToLose()
    {
        losing = true;
        yield return new WaitForSeconds(enemy.loseTime);
        lostPlayer = true;
        losing = false;
        Debug.Log("Tempo passou, perdeu o jogador.");
    }


    bool IsVisibleInCamera()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(enemy.transform.position);
        return viewPos.x >= 0 && viewPos.x <= 1 &&
           viewPos.y >= 0 && viewPos.y <= 1 &&
           viewPos.z > 0;
    }

}


