using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerStates
{
    private float walkSpeed = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) player.ChangeVerticalState<JumpingState>();
    }

    private void FixedUpdate()
    {

        HandleIdle();
        HandleInput();

        rb.velocity = new Vector2(player.getMoveInput() * walkSpeed, rb.velocity.y);

        // Inverte o jogador dependendo da direção
        player.FlipPlayer(player.getMoveInput());
    }

    public override void HandleInput()
    {
       base.HandleInput();
       if(Input.GetKey(KeyCode.LeftShift) && player.getMoveInput() != 0) player.ChangeHorizontalState<RunningState>();
    }

    /*
    private float walkSpeed;

    private void Start()
    {
       walkSpeed = player.walkSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            player.ChangeVerticalState(player.jumping);
        }
    }

    void FixedUpdate()
    {
        // Atualiza a velocidade
        player.rb.velocity = new Vector2(player.moveInput * walkSpeed, player.rb.velocity.y);

        // Inverte o jogador dependendo da direção
        player.FlipPlayer(player.moveInput);
    }
    */
}
