using UnityEngine;
using System.Collections;

public class PatrolState : EnemyStates
{
    [SerializeField] private float speed = 2f;          // Velocidade do inimigo
    [SerializeField] private float patrolDistance = 5f; // Distância total a percorrer
    [SerializeField] private float waitTime = 2f;       // Tempo parado antes de inverter a direção

    private Vector2 startPosition;    // Posição inicial do inimigo
    private bool isWaiting = false;

    void Start()
    {
        startPosition = transform.position; // Armazena a posição inicial
    }

    void Update()
    {
        if (!isWaiting) // Só move se não estiver esperando
        {
            float distanceTraveled = Vector2.Distance(startPosition, transform.position);

            if (distanceTraveled >= patrolDistance)
            {
                StartCoroutine(WaitBeforeFlip()); // Inicia a espera antes de inverter
            }

            transform.Translate(Vector2.right * (detection.isFacingRight() ? speed : -speed) * Time.deltaTime);
        }
    }

    IEnumerator WaitBeforeFlip()
    {
        isWaiting = true;  // Ativa o estado de espera
        yield return new WaitForSeconds(waitTime); // Espera o tempo especificado
        detection.Flip(); // Inverte a direção após a espera
        startPosition = transform.position; // Define a nova posição inicial para o próximo ciclo
        isWaiting = false; // Sai do estado de espera
    }
}
