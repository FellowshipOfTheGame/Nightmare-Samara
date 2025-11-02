using UnityEngine;

public class State : ScriptableObject
{
    public virtual void Enter(Player player) { }
    public virtual void Exit(Player player) { }
    public virtual void FrameUpdate(Player player) { }
    public virtual void PhysicsUpdate(Player player) { }
}
