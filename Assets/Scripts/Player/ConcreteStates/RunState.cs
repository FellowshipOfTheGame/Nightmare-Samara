using NUnit.Framework.Interfaces;
using UnityEngine;

[CreateAssetMenu(fileName = "RunState", menuName = "Concrete States/Player/Run State")]
public class RunState : State
{
    [Header("Transitions")]
    public State idleState;
    public State walkState;
    public State fallState;
    public State jumpState;

    [Header("Variables")]
    public float runSpeed = 7f;
    public float acceleration = 100f;

    public override void Enter(Player player)
    {
        Debug.Log("Entrando no RunState");
    }

    public override void Exit(Player player)
    {
        Debug.Log("Saindo do RunState");
    }


    public override void FrameUpdate(Player player)
    {
        player.Flip();
        float input = player.inputSystem.MoveInput.x;

        if (!player.isGrounded)
        {
            player.stateMachine.ChangeState(fallState);
            return;
        }
        if (player.inputSystem.JumpInput && player.isGrounded)
        {
            player.stateMachine.ChangeState(jumpState);
            return;
        }
        if (input == 0)
        {
            if (Mathf.Abs(player.rb.linearVelocity.x) < 0.1f)
            {
                player.stateMachine.ChangeState(idleState);
                return;
            }
        }
        else if (!player.inputSystem.RunInput)
        {
            player.stateMachine.ChangeState(walkState);
            return;
        }
    }

    public override void PhysicsUpdate(Player player)
    {
        //if (player.currentStamina <= 0)
        //{
        //    stateMachine.ChangeState(player.exhaustedState);
        //    return;
        //}
        //else
        //{
        //    player.staminaSystem.LoseStamina(player.staminaDrainRate);
        //}

        float input = player.inputSystem.MoveInput.x;

        float currentVelocityX = player.rb.linearVelocity.x;
        float targetSpeed =  input * runSpeed;

        float speedDifference = targetSpeed - currentVelocityX;
        float accelerationRate = acceleration;

        if (Mathf.Sign(speedDifference) != Mathf.Sign(currentVelocityX) && currentVelocityX != 0)
        {
            accelerationRate = acceleration * 2.0f;
        }

        float effectiveAcceleration = accelerationRate * Time.fixedDeltaTime;
        float speedX = Mathf.MoveTowards(currentVelocityX, targetSpeed, effectiveAcceleration);

        player.rb.linearVelocity = new Vector2(speedX, player.rb.linearVelocity.y);
    }
}
