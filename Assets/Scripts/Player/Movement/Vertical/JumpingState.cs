using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    private bool hasJumped = false;
    public JumpingState(PlayerStateMachine stateMachine, GameObject player)
     : base(stateMachine, player) { }

    public override void Enter()
    {
        //Debug.Log("Entrou no estado Jumping");

        if (!hasJumped)
        {
            stateMachine.rb.velocity = new Vector2(stateMachine.rb.velocity.x, 0f); // zera o Y antes
            stateMachine.rb.AddForce(Vector2.up * stateMachine.getJumpForce(), ForceMode2D.Impulse); // pulo
            hasJumped = true;
        }
    }

    public override void Update()
    {
        if (stateMachine.isGrounded() && stateMachine.rb.velocity.y <= 0.01f)
        {
            float move = HandleInput();
            if (Mathf.Abs(move) > 0.1f)
                stateMachine.ChangeState(new WalkingState(stateMachine, player));
            else
                stateMachine.ChangeState(new IdleState(stateMachine, player));
        }
    }

    public override void FixedUpdate()
    {
        float move = HandleInput(); 
        stateMachine.FlipPlayer(move);

        float maxSpeed = stateMachine.getWalkSpeed();
        Vector2 velocity = stateMachine.rb.velocity;

    
        float clampedX = Mathf.Clamp(move * maxSpeed, -maxSpeed, maxSpeed);

      
        stateMachine.rb.velocity = new Vector2(clampedX, velocity.y);

        if (velocity.y < 0f)
        {
            float fallMultiplier = stateMachine.getFallMultiplier(); // você define isso
            stateMachine.rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }


}
