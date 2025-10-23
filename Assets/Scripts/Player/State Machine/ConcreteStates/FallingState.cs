using UnityEngine;

public class FallingState : PlayerState
{
    public FallingState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void FrameUpdate()
    {
        float input = HandleInput();
        if (player.inputSystem.AttackInput)
        {
            stateMachine.ChangeState(player.attackState);
            return;
        }
        if (player.isGrounded)
        {
            if (input != 0) {
                if (player.inputSystem.RunInput)
                {
                    stateMachine.ChangeState(player.runningState);
                    return;
                }
                else
                {
                    stateMachine.ChangeState(player.walkingState);
                    return;
                }

            }
            else
            {
                stateMachine.ChangeState(player.idleState);
                return;
            }
        }

        if (player.isWalled) { 
            stateMachine.ChangeState(player.wallSlideState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        FlipPlayer();
        float maxSpeed = player.walkSpeed;
        float targetX = HandleInput() * maxSpeed;

        float newVelocityX = Mathf.Lerp(player.rb.velocity.x, targetX, Time.fixedDeltaTime * 10f);

        player.rb.velocity = new Vector2(newVelocityX, player.rb.velocity.y);

        if (player.rb.velocity.y < 0f)
        {
            float fallMultiplier = player.fallMult;
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}