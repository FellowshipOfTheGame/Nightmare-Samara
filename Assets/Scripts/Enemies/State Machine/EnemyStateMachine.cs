using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    private EnemyState currentState;

    public void Start(EnemyState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(EnemyState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void FrameUpdate()
    {
        currentState.FrameUpdate();

    }

    public void PhysicsUpdate()
    {
        currentState.PhysicsUpdate();
    }

}
