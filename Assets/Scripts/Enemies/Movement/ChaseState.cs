using UnityEngine;

public class ChaseState : EnemyStates
{
    private void Update()
    {
        if (detection.LosePlayer())
        {
            enemy.ChangeState(enemy.patrolState);
            return;
        }
    }
}