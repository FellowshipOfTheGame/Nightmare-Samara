using UnityEngine;

public class WallSlideState : PlayerState
{
    private int wallDirection;

    public WallSlideState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }

    public override void Enter()
    {
        wallDirection = stateMachine.GetWallDirection();
        stateMachine.FlipPlayer(-wallDirection); // Olha para o lado oposto da parede
    }

    public override void Update()
    {
        float move = HandleInput();

        // Pulo na parede
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(new WallJumpState(stateMachine, player));
            return;
        }

        // Se encostou no chão
        if (stateMachine.isGrounded() && stateMachine.rb.velocity.y <= 0.01f)
        {
            if (Mathf.Abs(move) > 0.1f)
                stateMachine.ChangeState(new WalkingState(stateMachine, player));
            else
                stateMachine.ChangeState(new IdleState(stateMachine, player));
            return;
        }

        // Se não está mais encostando na parede, começa a cair normalmente
        if (stateMachine.GetWallDirection() == 0)
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
            return;
        }
    }

    public override void FixedUpdate()
    {
        Vector2 vel = stateMachine.rb.velocity;
        float clampedY = Mathf.Clamp(vel.y, -stateMachine.getWallSlideSpeed(), float.MaxValue);
        stateMachine.rb.velocity = new Vector2(vel.x, clampedY);
    }
}
