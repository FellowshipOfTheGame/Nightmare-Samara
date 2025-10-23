
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void FrameUpdate()
    {
        float input = HandleInput();
        if (player.inputSystem.JumpInput && player.isGrounded)
        {
            stateMachine.ChangeState(player.jumpingState);
            return;
        }
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.fallingState);
            return;
        }

        if (input != 0)
        {
            if (player.inputSystem.RunInput)
            {
                stateMachine.ChangeState(player.runningState);
            }
            else
            {
                stateMachine.ChangeState(player.walkingState);
            }
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        
        player.rb.velocity = new Vector2(0f, player.rb.velocity.y);
        player.staminaSystem.GainStamina(player.staminaRegenRate * 0.8f);
    }
}