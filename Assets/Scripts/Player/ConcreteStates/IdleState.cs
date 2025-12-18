using UnityEngine;


[CreateAssetMenu(fileName = "IdleState", menuName = "Concrete States/Player/Idle State")]
public class IdleState : State
{
    [Header("Transitions")]
    public State walkState;
    public State runState;
    public State fallState;
    public State jumpState;

    public override void Enter(Player player)
    {
        //Debug.Log("Entrando no IdleState");
    }

    public override void Exit(Player player)
    {
        //Debug.Log("Saindo do IdleState");
    }

    public override void FrameUpdate(Player player)
    {
        float input = player.inputSystem.MoveInput.x;

        if (!player.isGrounded)
        {
            player.stateMachine.ChangeState(fallState);
            return;
        }
        if (player.inputSystem.JumpInput && player.isGrounded)
        {
            player.stateMachine.ChangeState(jumpState);
            return;
        }
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
    }
       
    public override void PhysicsUpdate(Player player)
    {
        player.rb.linearVelocity = new Vector2(0f, player.rb.linearVelocity.y);
    }
}

