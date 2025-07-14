using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    private bool hasJumped = false;
    private float staminaLoss => stateMachine.getJumpingStaminaLoss();

    public JumpingState(PlayerStateMachine stateMachine, GameObject player)
        : base(stateMachine, player) { }

    public override void Enter()
    {
        if (!hasJumped)
        {
            stateMachine.rb.velocity = new Vector2(stateMachine.rb.velocity.x, 0f); // Zera Y antes do impulso
            stateMachine.rb.AddForce(Vector2.up * stateMachine.getJumpForce(), ForceMode2D.Impulse);
            stateMachine.LossStamina(staminaLoss);
            hasJumped = true;
        }
    }

    public override void Update()
    {
        float move = HandleInput();

        // Começou a cair -> transição para FallingState
        if (isFalling() && stateMachine.rb.velocity.y < 0f)
        {
            stateMachine.ChangeState(new FallingState(stateMachine, player));
            return;
        }
        else if (stateMachine.IsWalled(HandleInput()))
        {
            stateMachine.ChangeState(new WallSlideState(stateMachine, player));
            return;
        }

        // (opcional: transição para WallSlide na subida, se quiser permitir pulo em parede para trás)
        // if (Mathf.Abs(move) > 0.1f && stateMachine.IsWalled(move))
        // {
        //     stateMachine.ChangeState(new WallSlideState(stateMachine, player));
        //     return;
        // }
    }

    public override void FixedUpdate()
    {
        float move = HandleInput();
        stateMachine.FlipPlayer(move);

        // Movimento horizontal no ar
        float maxSpeed = stateMachine.getWalkSpeed();
        Vector2 velocity = stateMachine.rb.velocity;
        float clampedX = Mathf.Clamp(move * maxSpeed, -maxSpeed, maxSpeed);
        stateMachine.rb.velocity = new Vector2(clampedX, velocity.y);

        // Pulo curto se jogador soltar espaço
        if (velocity.y > 0f && !Input.GetKey(KeyCode.Space))
        {
            float lowJumpMultiplier = stateMachine.getLowJumpMultiplier();
            stateMachine.rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}
