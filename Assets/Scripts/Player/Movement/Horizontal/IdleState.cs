using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    private float staminaGain => stateMachine.getIdleStaminaGain();
    private bool exhausted = false;

    //Construtor
    public IdleState(PlayerStateMachine stateMachine, GameObject player)
        : base(stateMachine, player) { }

    public override void Update()
    {
        if(stateMachine.getCurrentStamina() == 0)
        {
            stateMachine.ChangeState(new ExhaustedState(stateMachine,player));
        }

        //Transiciona para o estado de jumping
        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.isGrounded())
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
            return;
        }
        //Transiciona para o estado de walking
        if (HandleInput() != 0)
        {
            stateMachine.ChangeState(new WalkingState(stateMachine, player));
        }

        if (HandleInput() != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(new RunningState(stateMachine, player));
        }

        //if (stateMachine.rb.velocity.y < 0f)
        //{
        //    stateMachine.ChangeState(new FallingState(stateMachine, player));
        //    return;
        //}
    }

    public override void FixedUpdate()
    {
        //Zera a velocidade em x para fazer com que o player pare instantaneamente e nao deslize
        if (Mathf.Abs(HandleInput()) < 0.01f){
            Rigidbody2D rb = stateMachine.rb;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (stateMachine.hasLackOfStamina())
        {
            stateMachine.GainStamina(staminaGain);
        }

        
    }
}

