using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class PlayerState
{

    protected Player player; 
    protected PlayerStateMachine stateMachine;
    

    public PlayerState(Player player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual float HandleInput()
    {
        return player.inputSystem.MoveInput.x;
    }

    public virtual void FlipPlayer()
    {
        float input = HandleInput();
        if (input > 0)
            player.transform.localScale = new Vector2(Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y);
        else if (input < 0)
            player.transform.localScale = new Vector2(-Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y);
    }
}
