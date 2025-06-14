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
       
    }

    public override void FixedUpdate()
    {
        Vector2 vel = stateMachine.rb.velocity;
        float clampedY = Mathf.Clamp(vel.y, -stateMachine.getWallSlideSpeed(), float.MaxValue);
        stateMachine.rb.velocity = new Vector2(vel.x, clampedY);
    }
}
