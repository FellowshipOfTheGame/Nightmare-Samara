using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerState
{

    private float moveInput;

    public RunningState(PlayerStateMachine stateMachine, GameObject player)
     : base(stateMachine, player) { }

    public override void Enter()
    {
        Debug.Log("Entrou no estado Running");
    }
    /*
    public override void Update()
    {

        moveInput = HandleInput();
        stateMachine.FlipPlayer(moveInput);

        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.isGrounded())
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
            return;
        }

        // Transições de estado:
        if (Mathf.Abs(moveInput) < 0.01f)
        {
            stateMachine.ChangeState(new IdleState(stateMachine, player));
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.ChangeState(new WalkingState(stateMachine, player));
        }
    }

    public override void FixedUpdate()
    {
        moveInput = HandleInput();
        Rigidbody2D rb = stateMachine.rb;

        if (Mathf.Abs(moveInput) < 0.01f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return;
        }
        float targetSpeed = moveInput * stateMachine.getRunSpeed();
        rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
    }
    */
}
