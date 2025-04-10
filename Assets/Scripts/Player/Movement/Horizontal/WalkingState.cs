using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerState
{
    private float moveInput;

    public WalkingState(PlayerStateMachine stateMachine, GameObject player)
        : base(stateMachine, player) { }

    public override void Enter()
    {
        //Debug.Log("Entrou no estado Walking");
    }

    public override void Update()
    {
        moveInput = HandleInput();
        stateMachine.FlipPlayer(moveInput);

        if (Input.GetKeyDown(KeyCode.Space) && stateMachine.isGrounded())
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
            return;
        }

        if (Mathf.Abs(moveInput) < 0.01f)
        {
            stateMachine.ChangeState(new IdleState(stateMachine, player));
        }
    }

    public override void FixedUpdate()
    {
        Rigidbody2D rb = stateMachine.rb;
        float targetSpeed = moveInput * stateMachine.getWalkSpeed();
        float speedDiff = targetSpeed - rb.velocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f)
            ? stateMachine.getAcceleration()
            : stateMachine.getDeceleration();

        float movement = speedDiff * accelRate * Time.fixedDeltaTime;

        rb.velocity = new Vector2(rb.velocity.x + movement, rb.velocity.y);

        if (Mathf.Abs(rb.velocity.x) < 0.05f && Mathf.Abs(moveInput) < 0.01f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
}
