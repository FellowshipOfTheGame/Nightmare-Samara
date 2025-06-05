using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : PlayerState
{
    public WallSlideState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player)
    { }

    public override void Update()
    {
        float move = HandleInput();
        Debug.Log("Estado de WALL SLIDE");
        if (stateMachine.isGrounded() && stateMachine.rb.velocity.y <= 0.01f)
        {

            if (Mathf.Abs(move) > 0.1f)
                stateMachine.ChangeState(new WalkingState(stateMachine, player));
            else
                stateMachine.ChangeState(new IdleState(stateMachine, player));
        }
    }

    public override void FixedUpdate()
    {
        stateMachine.rb.velocity = new Vector2(stateMachine.rb.velocity.x, Mathf.Clamp(stateMachine.rb.velocity.y, -stateMachine.getWallSlideSpeed(), float.MaxValue));
    }
}
