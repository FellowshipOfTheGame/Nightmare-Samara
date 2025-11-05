using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Patrol", story: "[Enemy] patrols", category: "Action", id: "e4f7066ffb3cc0b37b23797bb75b51ef")]
public partial class PatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    private Enemy enemyController;
    [SerializeReference] public float patrolSpeed = 2f;

    protected override Status OnStart()
    {
        enemyController = Enemy.Value.GetComponent<Enemy>();
        if (enemyController == null) {
            return Status.Failure;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        bool flip = enemyController.sceneCheck.CheckForObstacles();

        if (!flip) {

            enemyController.rb.linearVelocity = new Vector2(enemyController.transform.localScale.x * patrolSpeed, enemyController.rb.linearVelocity.y);
            return Status.Running;
        }
        else
        {
            enemyController.rb.linearVelocity = new Vector2(0, enemyController.rb.linearVelocity.y);
            return Status.Success;
        }
        
    }

    protected override void OnEnd()
    {
    }
}

