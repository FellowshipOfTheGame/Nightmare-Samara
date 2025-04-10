using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected GameObject player;

    public PlayerState(PlayerStateMachine stateMachine, GameObject player)
    {
        this.stateMachine = stateMachine;
        this.player = player;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual float HandleInput() {
        return Input.GetAxisRaw("Horizontal");
    }
}
