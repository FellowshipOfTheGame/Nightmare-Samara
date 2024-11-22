using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovHandler : MonoBehaviour
{
    public Transform player; // Referência ao Transform do jogador
    public float detectionRadius = 5f; // Distância para detectar o jogador
    public float speed = 2f; // Velocidade de perseguição

    private bool isChasing = false;

    void Update()
    {
        // Calcula a distância entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Verifica se o jogador está dentro do raio de detecção
        if (distanceToPlayer <= detectionRadius || isChasing )
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // Se está perseguindo, move o inimigo em direção ao jogador
        if (isChasing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    // Desenhar o raio de detecção no Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
