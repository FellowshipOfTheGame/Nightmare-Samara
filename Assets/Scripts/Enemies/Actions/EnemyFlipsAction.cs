using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Enemy flips", story: "[Enemy] flip", category: "Action", id: "cdee601ab6145120292cbdf5f9777b4b")]
public partial class EnemyFlipsAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    private Enemy enemyController;

    protected override Status OnStart()
    {
        enemyController = Enemy.Value.GetComponent<Enemy>();
        if (enemyController == null)
        {
            return Status.Failure;
        }
        enemyController.Flip();
        return Status.Success;
    }

}

