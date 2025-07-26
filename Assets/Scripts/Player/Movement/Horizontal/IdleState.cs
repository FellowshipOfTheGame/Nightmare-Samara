// IdleState.cs CORRIGIDO

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    //private float staminaGain => stateMachine.getIdleStaminaGain();

    public IdleState(PlayerStateMachine stateMachine, GameObject player)
        : base(stateMachine, player) { }

    public override void Update()
    {
        // A hierarquia de checagens é importante.
        // As ações mais "ativas" (pular, cair) devem vir primeiro.
        if (isFalling())
        {
            stateMachine.ChangeState(new FallingState(stateMachine, player));
        }
        else if (isJumping())
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
        }
        // Dê prioridade para a corrida sobre a caminhada
        else if (isRunning())
        {
            stateMachine.ChangeState(new RunningState(stateMachine, player));
        }
        else if (isWalking())
        {
            stateMachine.ChangeState(new WalkingState(stateMachine, player));
        }
        /*
        else if (isExhausted())
        {
            stateMachine.ChangeState(new ExhaustedState(stateMachine, player));
        }
        */
    }

    public override void FixedUpdate()
    {
        if (Mathf.Abs(HandleInput()) < 0.01f)
        {
            Rigidbody2D rb = stateMachine.rb;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        /*
        if (stateMachine.hasLackOfStamina())
        {
            stateMachine.GainStamina(staminaGain);
        }
        */
    }
}