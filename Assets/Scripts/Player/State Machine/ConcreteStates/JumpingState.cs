using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    public JumpingState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0f);
        player.rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);
    }

    public override void FrameUpdate()
    {
        if (player.rb.velocity.y < 0f)
        {
            stateMachine.ChangeState(player.fallingState);
            return;
        }
        //else if (stateMachine.IsWalled(HandleInput()))
        //{
        //    stateMachine.ChangeState(new WallSlideState(stateMachine, player));
        //    return;
        //}

    }

    public override void PhysicsUpdate()
    {
        FlipPlayer();
        float move = HandleInput();

        float maxSpeed = player.walkSpeed ;
        Vector2 velocity = player.rb.velocity;
        float clampedX = Mathf.Clamp(move * maxSpeed, -maxSpeed, maxSpeed);
        player.rb.velocity = new Vector2(clampedX, velocity.y);

        if (velocity.y > 0f && !Input.GetKey(KeyCode.Space))
        {
            float lowJumpMultiplier = player.lowJumpMult;
            player.rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}
