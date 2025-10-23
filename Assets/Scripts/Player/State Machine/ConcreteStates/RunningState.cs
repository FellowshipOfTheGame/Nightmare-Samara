using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerState
{
    public RunningState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void FrameUpdate()
    {
        FlipPlayer();
        float input = HandleInput();
        if (player.inputSystem.AttackInput)
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.fallingState);
            return;
        }

        if (player.inputSystem.JumpInput)
        {
            stateMachine.ChangeState(player.jumpingState);
            return;
        }

        if (input == 0)
        {
            if (Mathf.Abs(player.rb.velocity.x) < 0.1f)
            {
                stateMachine.ChangeState(player.idleState);
                return;
            }
        }
        else if (!player.inputSystem.RunInput)
        {
            stateMachine.ChangeState(player.walkingState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {

        if (player.currentStamina <= 0)
        {
            stateMachine.ChangeState(player.exhaustedState);
            return;
        }
        else
        {
            player.staminaSystem.LoseStamina(player.staminaDrainRate);
        }

        float currentVelocityX = player.rb.velocity.x;
        float targetSpeed = HandleInput() * player.runSpeed;

        // Acelerando ou desacelerando
        float speedDifference = targetSpeed - currentVelocityX;
        float accelerationRate = player.acceleration;

        // Aumenta a aceleração ao virar para dar mais controle
        if (Mathf.Sign(speedDifference) != Mathf.Sign(currentVelocityX) && currentVelocityX != 0)
        {
            accelerationRate = player.acceleration * 2.0f;
        }

        float effectiveAcceleration = accelerationRate * Time.fixedDeltaTime;
        float speedX = Mathf.MoveTowards(currentVelocityX, targetSpeed, effectiveAcceleration);

        player.rb.velocity = new Vector2(speedX, player.rb.velocity.y);
    }
}