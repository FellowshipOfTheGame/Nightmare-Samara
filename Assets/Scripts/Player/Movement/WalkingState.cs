using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerStates
{

    [SerializeField] private float moveSpeed = 5f;
    private float moveInput;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
        {
            player.ChangeVerticalState(player.jumping);
        }
    }

    void FixedUpdate()
    {
        // Movimento Horizontal
        moveInput = 0f;

        if (Input.GetKey(KeyCode.D)) moveInput = 1f;
        if (Input.GetKey(KeyCode.A)) moveInput = -1f;
        

        // Atualiza a velocidade
        player.rb.velocity = new Vector2(moveInput * moveSpeed, player.rb.velocity.y);

        // Inverte o jogador dependendo da direção
        player.FlipPlayer(moveInput);
    }
}
