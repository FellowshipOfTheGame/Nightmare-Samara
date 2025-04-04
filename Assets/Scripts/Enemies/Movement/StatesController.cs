using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesController : MonoBehaviour
{
    public PatrolState patrolState;
    public ChaseState chaseState;
    public EnemyStates currentState;

    void Start()
    {
        // Começa no estado de patrulha
        ChangeState(patrolState);
    }

    public void ChangeState(EnemyStates newState)
    {
        if (currentState == newState) return;

        if (currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
