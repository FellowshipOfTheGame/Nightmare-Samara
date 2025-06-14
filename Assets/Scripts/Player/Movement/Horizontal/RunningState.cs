// RunningState.cs CORRIGIDO

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerState
{
    private float moveInput;
    private float staminaLoss => stateMachine.getRunningStaminaLoss();

    public RunningState(PlayerStateMachine stateMachine, GameObject player)
        : base(stateMachine, player) { }

    public override void Update()
    {
        moveInput = HandleInput();
        stateMachine.FlipPlayer(moveInput);

        // Hierarquia de prioridades para sair do estado de corrida
        if (isFalling())
        {
            stateMachine.ChangeState(new FallingState(stateMachine, player));
        }
        else if (isExhausted())
        {
            stateMachine.ChangeState(new ExhaustedState(stateMachine, player));
        }
        else if (isJumping())
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
        }
        // Se soltar o Shift mas continuar se movendo, mude para Walking
        else if (isWalking())
        {
            stateMachine.ChangeState(new WalkingState(stateMachine, player));
        }
        // Se parar de se mover completamente, mude para Idle
        else if (!isMoving())
        {
            stateMachine.ChangeState(new IdleState(stateMachine, player));
        }
    }

    public override void FixedUpdate()
    {
        // ... (seu código de aceleração e movimento continua o mesmo)
        float currentVelocityX = stateMachine.rb.velocity.x;
        float targetSpeed = moveInput * stateMachine.getRunSpeed();
        float acceleration = stateMachine.getAcceleration();
        bool turning = (currentVelocityX != 0f && Mathf.Sign(currentVelocityX) != Mathf.Sign(targetSpeed));
        float effectiveAcceleration = turning ? acceleration * 0.5f : acceleration;
        float speedX = Mathf.MoveTowards(currentVelocityX, targetSpeed, effectiveAcceleration * Time.fixedDeltaTime);
        stateMachine.rb.velocity = new Vector2(speedX, stateMachine.rb.velocity.y);

        stateMachine.LossStamina(staminaLoss);
    }
}