using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesController : MonoBehaviour
{
    public PatrolState patrolState;
    public ChaseState chaseState;

    [SerializeField] private Transform player; // Referência ao Transform do jogador
    [SerializeField] private float detectionRadius = 5f;

    public EnemyStates currentState;

    void Start()
    {
        // Começa no estado de patrulha
        ChangeState(patrolState);
    }

    public void ChangeState(EnemyStates newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public bool SeesPlayer()
    {
        // Lógica para detectar o jogador
        return Vector2.Distance(transform.position, player.position) < 5f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
