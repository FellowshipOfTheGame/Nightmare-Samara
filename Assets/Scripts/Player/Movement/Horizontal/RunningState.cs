using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerStates
{

    private float runSpeed = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) player.ChangeVerticalState<JumpingState>();
    }

    private void FixedUpdate()
    {
        Debug.Log("Running");

        HandleIdle();
        HandleInput();

        rb.velocity = new Vector2(player.getMoveInput() * runSpeed, rb.velocity.y);

        // Inverte o jogador dependendo da direção
        player.FlipPlayer(player.getMoveInput());
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (!Input.GetKey(KeyCode.LeftShift) && player.getMoveInput() != 0) player.ChangeHorizontalState<WalkingState>();
    }

    /*
    private float runSpeed;
    private float moveInput;

    private void Start()
    {
        runSpeed = player.runSpeed;
    }

    private void Update()
    {
        if (player.stamina > 0) {
            
            player.stamina -= player.drainRate * Time.deltaTime;
            player.stamina = Mathf.Max(player.stamina, 0f);
        }
        else
        {
            player.ChangeHorizontalState(player.walking);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            player.ChangeVerticalState(player.jumping);
        }

    }

    private void FixedUpdate()
    {
        if (!Input.GetKeyDown(KeyCode.LeftShift)) player.ChangeHorizontalState(player.walking);


        moveInput = 0f;

        if (Input.GetKey(KeyCode.D)) moveInput = 1f;
        if (Input.GetKey(KeyCode.A)) moveInput = -1f;

        player.rb.velocity = new Vector2(moveInput * runSpeed, player.rb.velocity.y);
        player.FlipPlayer(moveInput);
    }
    */
}
