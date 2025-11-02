using UnityEngine;

[CreateAssetMenu(fileName = "JumpState", menuName = "Concrete States/Player/Jump State")]
public class JumpState : State
{
    [Header("Transitions")]
    public State fallState;

    [Header("Variables")]
    public float moveSpeed = 5f;
    public float jumpForce = 3f;
    public float lowJumpMult = 2f;
    public float fallMult = 2.5f;

    public override void Enter(Player player)
    {
        Debug.Log("Entrando no JumpState");
        player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, 0f);
        player.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public override void Exit(Player player)
    {
        Debug.Log("Saindo do JumpState");
    }

    public override void FrameUpdate(Player player)
    {
        player.Flip();
        if (player.rb.linearVelocity.y < 0f)
        {
            player.stateMachine.ChangeState(fallState);
            return;
        }
    }

    public override void PhysicsUpdate(Player player)
    {
        float input = player.inputSystem.MoveInput.x;

        float maxSpeed = moveSpeed;
        Vector2 velocity = player.rb.linearVelocity;
        float clampedX = Mathf.Clamp(input * maxSpeed, -maxSpeed, maxSpeed);
        player.rb.linearVelocity = new Vector2(clampedX, velocity.y);

        if (velocity.y > 0f && player.inputSystem.JumpInput)
        {
            float lowJumpMultiplier = lowJumpMult;
            player.rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}
