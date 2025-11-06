using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "FallState", menuName = "Concrete States/Player/Fall State")]
public class FallState : State
{
    [Header("Transitions")]
    public State idleState;
    public State walkState;
    public State runState;

    [Header("Variables")]
    public float moveSpeed = 5f;
    public float fallMult = 2.5f;

    public override void Enter(Player player)
    {
        //Debug.Log("Entrando no FallState");
    }

    public override void Exit(Player player)
    {
        //Debug.Log("Saindo do FallState");
       
    }

    public override void FrameUpdate(Player player)
    {
        player.Flip();
        float input = player.inputSystem.MoveInput.x;

        if (input != 0)
        {
            if (player.inputSystem.RunInput)
            {
                player.stateMachine.ChangeState(runState);
                return;
            }
            else
            {
                player.stateMachine.ChangeState(walkState);
                return;
            }
        }
        else{
            player.stateMachine.ChangeState(idleState);
            return;
        }

    }

    public override void PhysicsUpdate(Player player)
    {
        float input = player.inputSystem.MoveInput.x;

        float maxSpeed = moveSpeed;
        float targetX = input * maxSpeed;

        float newVelocityX = Mathf.Lerp(player.rb.linearVelocity.x, targetX, Time.fixedDeltaTime * 10f);

        player.rb.linearVelocity = new Vector2(newVelocityX, player.rb.linearVelocity.y);

        if (player.rb.linearVelocity.y < 0f)
        {
            float fallMultiplier = fallMult;
            player.rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}
