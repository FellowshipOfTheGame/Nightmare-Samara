using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : PlayerState
{
    public WallSlideState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void FrameUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (!player.isWalled || player.isGrounded)
        {
            stateMachine.ChangeState(player.fallingState);
        }

        float input = HandleInput();

        if ( input != 0 && Mathf.Sign(input) != Mathf.Sign(player.wallDirection))
        {
            stateMachine.ChangeState(player.fallingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, -player.wallSlideSpeed);
    }
}
