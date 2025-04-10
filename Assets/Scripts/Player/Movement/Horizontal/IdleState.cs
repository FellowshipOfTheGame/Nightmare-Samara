using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine, GameObject player)
        : base(stateMachine, player) { }

    public override void Enter()
    {
        //Debug.Log("Entrou no estado Idle");
    }

    public override void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.isGrounded())
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
            return;
        }

        if (HandleInput() != 0 ) 
        {
            stateMachine.ChangeState(new WalkingState(stateMachine, player));
        }
    }
}

