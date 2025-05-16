using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerState
{

    private float moveInput;

    private float staminaLoss => stateMachine.getRunningStaminaLoss();

    public RunningState(PlayerStateMachine stateMachine, GameObject player)
     : base(stateMachine, player) { }

    public override void Enter()
    {
        //Debug.Log("Entrou no estado Running");
    }

    public override void Update()
    {
        moveInput = HandleInput(); // Pega a direção do input do usuario
        stateMachine.FlipPlayer(moveInput); // Flipa o player de acordo com a direção

        // Pula se estiver no chão e apertar espaço
        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.isGrounded())
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
            return;
        }

        // Sai da corrida se soltar o botão de movimento
        if (Mathf.Abs(moveInput) < 0.01f || stateMachine.getCurrentStamina() == 0)
        {
            stateMachine.ChangeState(new IdleState(stateMachine, player));
            return;
        }

        // Se ainda está se movendo, mas não segurando SHIFT, vai para caminhada
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(new WalkingState(stateMachine, player));
            return;
        }
    }

    public override void FixedUpdate()
    {
        float currentVelocityX = stateMachine.rb.velocity.x;
        float targetSpeed = moveInput * stateMachine.getRunSpeed();
        float acceleration = stateMachine.getAcceleration();

        // Verifica se está mudando de direção (sinais opostos)
        bool turning = (currentVelocityX != 0f && Mathf.Sign(currentVelocityX) != Mathf.Sign(targetSpeed));

        float effectiveAcceleration = turning ? acceleration * 0.5f : acceleration; // reduz aceleração ao virar

        float speedX = Mathf.MoveTowards(currentVelocityX, targetSpeed, effectiveAcceleration * Time.fixedDeltaTime);

        stateMachine.rb.velocity = new Vector2(speedX, stateMachine.rb.velocity.y);

        stateMachine.LossStamina(staminaLoss);

    }

}
