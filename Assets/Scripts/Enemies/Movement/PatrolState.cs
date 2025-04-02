using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyStates
{
    void Update()
    {
        Debug.Log("Patrulhando...");
        if (enemy.SeesPlayer())
        {
            enemy.ChangeState(enemy.chaseState);
        }
        
    }
}
