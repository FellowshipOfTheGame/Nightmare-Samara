using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    private bool hasJumped = false;

    private float staminaLoss => stateMachine.getJumpingStaminaLoss();

    //Construtor 
    public JumpingState(PlayerStateMachine stateMachine, GameObject player)
     : base(stateMachine, player) { }

    public override void Enter()
    {
        //Debug.Log("Entrou no estado Jumping");

        //Verifica se ainda esta no meio do pulo para evitar mais de um pulo no meio do ar
        if (!hasJumped)
        {
            stateMachine.rb.velocity = new Vector2(stateMachine.rb.velocity.x, 0f); // zera o Y antes
            stateMachine.rb.AddForce(Vector2.up * stateMachine.getJumpForce(), ForceMode2D.Impulse); // pulo
            hasJumped = true;
            stateMachine.LossStamina(staminaLoss);
        }
    }

    public override void Update()
    {
        //Verifica se esta no chão e se nao tem velocidade em y para trocar para os estados de walking ou de idle
        if (stateMachine.isGrounded() && stateMachine.rb.velocity.y <= 0.01f)
        {
            float move = HandleInput();
            if (Mathf.Abs(move) > 0.1f)
                stateMachine.ChangeState(new WalkingState(stateMachine, player));
            else
                stateMachine.ChangeState(new IdleState(stateMachine, player));
        }
    }

    public override void FixedUpdate()
    {
        float move = HandleInput();
        stateMachine.FlipPlayer(move);

        float maxSpeed = stateMachine.getWalkSpeed();
        Vector2 velocity = stateMachine.rb.velocity;

        float clampedX = Mathf.Clamp(move * maxSpeed, -maxSpeed, maxSpeed);
        stateMachine.rb.velocity = new Vector2(clampedX, velocity.y);

        // 🌠 Subida mais rápida OU pulo curto se soltar espaço
        if (velocity.y > 0f)
        {
            float lowJumpMultiplier = stateMachine.getLowJumpMultiplier();

            // Se o jogador soltou o botão de pulo no meio do salto, aplica gravidade extra
            if (!Input.GetKey(KeyCode.Space))
            {
                stateMachine.rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }
        }

        // ⬇️ Queda mais rápida
        if (velocity.y < 0f)
        {
            float fallMultiplier = stateMachine.getFallMultiplier();
            stateMachine.rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}
