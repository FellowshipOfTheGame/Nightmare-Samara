using UnityEngine;

public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }

    public override void Update()
    {
        if (stateMachine.isGrounded())
        {
            if (isRunning())
            {
                stateMachine.ChangeState(new RunningState(stateMachine, player));
            }
            else if (isWalking())
            {
                stateMachine.ChangeState(new WalkingState(stateMachine, player));
            }
            else
            {
                stateMachine.ChangeState(new IdleState(stateMachine, player));
            }
            return;
        }
        else if (stateMachine.IsWalled(HandleInput())) { 
            stateMachine.ChangeState(new WallSlideState(stateMachine, player));
            return;
        }

    }

    public override void FixedUpdate()
    {
        float move = HandleInput();
        stateMachine.FlipPlayer(move);

        float maxSpeed = stateMachine.getWalkSpeed();
        float targetX = move * maxSpeed;

        float newVelocityX = Mathf.Lerp(stateMachine.rb.velocity.x, targetX, Time.fixedDeltaTime * 10f);

        stateMachine.rb.velocity = new Vector2(newVelocityX, stateMachine.rb.velocity.y);

        if (stateMachine.rb.velocity.y < 0f)
        {
            float fallMultiplier = stateMachine.getFallMultiplier();
            stateMachine.rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}