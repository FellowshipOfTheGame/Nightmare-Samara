using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "WalkState", menuName = "Concrete States/Player/Walk State")]
public class WalkState : State
{
    [Header("Transitions")]
    public State idleState;
    public State runState;
    public State fallState;
    public State jumpState;

    [Header("Variables")]
    public float walkSpeed = 5f;
    public float acceleration = 100f;

    public override void Enter(Player player)
    {
        Debug.Log("Entrando no WalkState");
    }

    public override void Exit(Player player)
    {
        Debug.Log("Saindo do WalkState");
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
        else if (player.inputSystem.RunInput)
        {
            player.stateMachine.ChangeState(runState);
            return;
        }
    }

    public override void PhysicsUpdate(Player player)
    {
        float input = player.inputSystem.MoveInput.x;

        float currentVelocityX = player.rb.linearVelocity.x;
        float targetSpeed = input * walkSpeed;

        float accelerationRate = acceleration;
        if (Mathf.Sign(targetSpeed) != Mathf.Sign(currentVelocityX) && currentVelocityX != 0)
        {
            accelerationRate = acceleration * 1.5f;
        }
        float effectiveAcceleration = accelerationRate * Time.fixedDeltaTime;
        float speedX = Mathf.MoveTowards(currentVelocityX, targetSpeed, effectiveAcceleration);

        player.rb.linearVelocity = new Vector2(speedX, player.rb.linearVelocity.y);
        //player.staminaSystem.GainStamina(player.staminaRegenRate * 0.5f);
    }
}
