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

    public override void Enter()
    {
        //Debug.Log("Entrou no estado Idle");
    }

    public override void Update()
    {
        //Debug.Log(exhausted);
        if(stateMachine.getCurrentStamina() == 0)
        {
            exhausted = true;
        }

        //Transiciona para o estado de jumping
        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.isGrounded() && !exhausted)
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
            return;
        }
        //Transiciona para o estado de walking
        if (HandleInput() != 0 && !exhausted)
        {
            stateMachine.ChangeState(new WalkingState(stateMachine, player));
        }

        if (HandleInput() != 0 && Input.GetKey(KeyCode.LeftShift) && !exhausted)
        {
            stateMachine.ChangeState(new RunningState(stateMachine, player));
        }
    }

    public override void FixedUpdate()
    {
        //Zera a velocidade em x para fazer com que o player pare instantaneamente e nao deslize
        if (Mathf.Abs(HandleInput()) < 0.01f ||exhausted){
            Rigidbody2D rb = stateMachine.rb;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (stateMachine.hasLackOfStamina())
        {
            stateMachine.GainStamina(staminaGain);
        }
        else
        {
            exhausted = false;
        }
        
    }
}

