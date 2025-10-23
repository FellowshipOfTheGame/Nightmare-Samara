using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerState
{
    public WalkingState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void FrameUpdate()
    {
        FlipPlayer();
        float input = HandleInput();

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

        // Lógica de transição mais fluida
        if (input == 0)
        {
            // Transição para ocioso se não houver input e a velocidade for baixa
            if (Mathf.Abs(player.rb.velocity.x) < 0.1f)
            {
                stateMachine.ChangeState(player.idleState);
                return;
            }
        }
        else if (player.inputSystem.RunInput)
        {
            stateMachine.ChangeState(player.runningState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        float currentVelocityX = player.rb.velocity.x;
        float targetSpeed = HandleInput() * player.walkSpeed;

        float accelerationRate = player.acceleration;
        if (Mathf.Sign(targetSpeed) != Mathf.Sign(currentVelocityX) && currentVelocityX != 0)
        {
            accelerationRate = player.acceleration * 1.5f;
        }
        float effectiveAcceleration = accelerationRate * Time.fixedDeltaTime;
        float speedX = Mathf.MoveTowards(currentVelocityX, targetSpeed, effectiveAcceleration);

        player.rb.velocity = new Vector2(speedX, player.rb.velocity.y);
        player.staminaSystem.GainStamina(player.staminaRegenRate * 0.5f);
    }
}
