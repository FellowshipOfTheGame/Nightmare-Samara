using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class ExhaustedState : PlayerState
{
    private float moveInput;

    public ExhaustedState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    //public override void Enter()
    //{
    //    Debug.Log("Entrando em exausto");
    //}

    //public override void Exit()
    //{
    //    Debug.Log("Saindo de exausto");
    //}

    public override void FrameUpdate()
    {
        FlipPlayer();
        float input = HandleInput();
        // Pulo baixo
        if (player.inputSystem.JumpInput && player.isGrounded)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.exhaustedJumpForce);
        }

        if (input == 0 && Mathf.Abs(player.rb.velocity.x) < 0.1f)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (player.currentStamina > player.minStamina)
        {
            stateMachine.ChangeState(player.runningState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        player.staminaSystem.GainStamina(player.staminaRegenRate);
        player.currentStamina = Mathf.Clamp(player.currentStamina, 0, player.maxStamina);

        float currentVelocityX = player.rb.velocity.x;
        float targetSpeed = HandleInput() * player.exhaustedWalkSpeed;
        float acceleration = player.acceleration;

        if (Mathf.Sign(targetSpeed) != Mathf.Sign(currentVelocityX) && currentVelocityX != 0)
        {
            acceleration *= 0.25f; 
        }
        else
        {
            acceleration *= 0.5f; 
        }

        float effectiveAcceleration = acceleration * Time.fixedDeltaTime;
        float speedX = Mathf.MoveTowards(currentVelocityX, targetSpeed, effectiveAcceleration);

        player.rb.velocity = new Vector2(speedX, player.rb.velocity.y);
    }
}
