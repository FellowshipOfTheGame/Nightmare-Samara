using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerState
{
    private float moveInput;

    //Construtor
    public WalkingState(PlayerStateMachine stateMachine, GameObject player)
        : base(stateMachine, player) { }

    public override void Enter()
    {
        //Debug.Log("Entrou no estado Walking");
    }

    public override void Update()
    {

        moveInput = HandleInput(); // Pega a direção do input do usuario
        stateMachine.FlipPlayer(moveInput); // Flipa o player de acordo com a direção

        //Transiciona para o estado de jumping
        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.isGrounded())
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
            return;
        }

        // Transiciona para o estado de idle
        if (Mathf.Abs(moveInput) < 0.01f)
        {
            stateMachine.ChangeState(new IdleState(stateMachine, player));
        }
       
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(new RunningState(stateMachine, player));
        }
       
    }

    public override void FixedUpdate()
    {
        //Realiza a movimentação do player
        Rigidbody2D rb = stateMachine.rb;
        float targetSpeed = moveInput * stateMachine.getWalkSpeed();
        rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
    }
}
