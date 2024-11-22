using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovHandler : MonoBehaviour
{
    public Transform player; // Refer�ncia ao Transform do jogador
    public float detectionRadius = 5f; // Dist�ncia para detectar o jogador
    public float speed = 2f; // Velocidade de persegui��o

    private bool isChasing = false;

    void Update()
    {
        // Calcula a dist�ncia entre o inimigo e o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Verifica se o jogador est� dentro do raio de detec��o
        if (distanceToPlayer <= detectionRadius || isChasing )
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // Se est� perseguindo, move o inimigo em dire��o ao jogador
        if (isChasing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    // Desenhar o raio de detec��o no Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
