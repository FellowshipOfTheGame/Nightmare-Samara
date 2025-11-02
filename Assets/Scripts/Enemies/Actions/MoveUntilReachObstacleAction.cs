using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveUntilReachObstacle", story: "[Enemy] moves", category: "Action/Navigation", id: "75c15f12f41c43fbf7c12ef35e9dfc40")]
public partial class MoveUntilReachObstacleAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    public float speed;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

