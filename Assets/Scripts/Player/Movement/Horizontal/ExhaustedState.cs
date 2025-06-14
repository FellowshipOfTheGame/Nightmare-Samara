using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class ExhaustedState : PlayerState
{
    private float staminaGain => stateMachine.getIdleStaminaGain();
    private bool exhausted = false;

    public ExhaustedState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player)
    {
    }

    public override void Update()
    {
        if (stateMachine.getCurrentStamina() == 0)
        {
            exhausted = true;
        }

        if (rested()) {
            stateMachine.ChangeState(new IdleState(stateMachine, player));
        }
    }

    public override void FixedUpdate()
    {
        //Zera a velocidade em x para fazer com que o player pare instantaneamente e nao deslize
        if (Mathf.Abs(HandleInput()) < 0.01f || exhausted)
        {
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
