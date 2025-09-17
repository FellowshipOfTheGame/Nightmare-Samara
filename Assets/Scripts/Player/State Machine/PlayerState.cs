using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public virtual float HandleInput() {
        float raw = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(raw) < 0.01f)
            return 0f;
        return Mathf.Sign(raw);
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
