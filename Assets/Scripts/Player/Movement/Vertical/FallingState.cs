using UnityEngine;

public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }


    public override void Update()
    {
        float move = HandleInput();

        // Se encostar na parede enquanto está caindo -> WallSlide
        if (!stateMachine.isGrounded() && Mathf.Abs(move) > 0.1f && stateMachine.IsWalled(move))
        {
            stateMachine.ChangeState(new WallSlideState(stateMachine, player));
            return;
        }

        // Tocou no chão -> Idle ou Walking
        if (stateMachine.isGrounded() && stateMachine.rb.velocity.y <= 0.01f)
        {
            if (Mathf.Abs(move) > 0.1f)
                stateMachine.ChangeState(new WalkingState(stateMachine, player));
            else
                stateMachine.ChangeState(new IdleState(stateMachine, player));
            return;
        }
    }

    public override void FixedUpdate()
    {
        float move = HandleInput();
        stateMachine.FlipPlayer(move);

        // Movimento horizontal durante a queda
        float maxSpeed = stateMachine.getWalkSpeed();
        Vector2 velocity = stateMachine.rb.velocity;
        float clampedX = Mathf.Clamp(move * maxSpeed, -maxSpeed, maxSpeed);
        stateMachine.rb.velocity = new Vector2(clampedX, velocity.y);

        // Gravidade extra para queda
        if (velocity.y < 0f)
        {
            float fallMultiplier = stateMachine.getFallMultiplier();
            stateMachine.rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}
