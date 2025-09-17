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
        // Aplica a força do pulo, empurrando para cima e na direção oposta à parede.
        // Usamos as propriedades 'wallJumpForce' e 'WallDirection' da classe Player.
        player.rb.velocity = new Vector2(player.wallJumpForce.x * -player.wallDirection, player.wallJumpForce.y);

        // Desativa o controle de movimento horizontal por um curto período
        player.canMove = false;

        // Aguarda o tempo de desabilitar o movimento
        yield return new WaitForSeconds(player.disableMoveTime);

        // Reativa o controle e muda para o estado de queda (FallingState)
        player.canMove = true;
        stateMachine.ChangeState(player.fallingState);
    }
}
