using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerState
{
    private float moveInput;

    //private float staminaGain => stateMachine.getWalkingStaminaGain();

    //Construtor
    public WalkingState(PlayerStateMachine stateMachine, GameObject player)
        : base(stateMachine, player) { }

    public override void Update()
    {

        moveInput = HandleInput(); // Pega a direção do input do usuario
        stateMachine.FlipPlayer(moveInput); // Flipa o player de acordo com a direção

        if (isFalling())
        {
            stateMachine.ChangeState(new FallingState(stateMachine, player));
            return;
        }
        else if (isJumping())
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
            return;
        }
        else  if (isRunning())
        {
            stateMachine.ChangeState(new RunningState(stateMachine, player));
            return;
        }
        else if (!isMoving())
        {
            stateMachine.ChangeState(new IdleState(stateMachine, player));
            return;
        }
        /*
        else if (isExhausted())
        {
            stateMachine.ChangeState(new ExhaustedState(stateMachine, player));
            return;
        }
        */
    }

    public override void FixedUpdate()
    {
        //Realiza a movimentação do player
        Rigidbody2D rb = stateMachine.rb;
        float targetSpeed = moveInput * stateMachine.getWalkSpeed();
        rb.velocity = new Vector2(targetSpeed, rb.velocity.y);

        //stateMachine.GainStamina(staminaGain);
    }
}
