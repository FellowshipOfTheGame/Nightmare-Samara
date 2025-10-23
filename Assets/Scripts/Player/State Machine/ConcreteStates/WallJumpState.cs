using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : PlayerState
{
    public WallJumpState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.StartCoroutine(PerformWallJump());
    }

    private IEnumerator PerformWallJump()
    {
        player.rb.velocity = new Vector2(player.wallJumpForce.x * -player.wallDirection, player.wallJumpForce.y);

        player.canMove = false;

        yield return new WaitForSeconds(player.disableMoveTime);

        player.canMove = true;
        stateMachine.ChangeState(player.fallingState);
    }
}
