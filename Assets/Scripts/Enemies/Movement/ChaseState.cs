using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(EnemyStateMachine stateMachine, GameObject enemy)
     : base(stateMachine,enemy) { }
}