using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerStates
{
    [SerializeField] private float jumpPower = 5f;
    private bool hasJumped = false;

    void Update()
    {
        if (!hasJumped)
        {
            player.rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            hasJumped = true;
        }

        // Volta pro estado de andar se estiver no chão
        if (player.isGrounded && player.rb.velocity.y <= 0.1f)
        {
            player.ChangeVerticalState(player.grounded);
        }
    }

    public override void Enter()
    {
        base.Enter();
        hasJumped = false;
    }
}
