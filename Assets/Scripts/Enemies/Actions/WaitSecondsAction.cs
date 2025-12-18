using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait seconds", story: "Wait [seconds]", category: "Action", id: "59a6188e3afee69956fb92786d8249c7")]
public partial class WaitSecondsAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Seconds;
    [CreateProperty]
    private float m_Timer = 0.0f;

    protected override Status OnStart()
    {
        if (Seconds == null || Seconds.Value <= 0.0f)
        {
            return Status.Success;
        }

        m_Timer = Seconds.Value;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        m_Timer -= Time.deltaTime;

        if (m_Timer <= 0.0f)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        m_Timer = 0.0f;
    }
}

