using UnityEngine;

public class ChaseState : EnemyState
{
    private UnityEngine.Transform player;
    private PlayerStateMachine playerSM;

    public ChaseState(EnemyStateMachine stateMachine, GameObject enemy)
     : base(stateMachine, enemy) { }

    private Rigidbody2D rb => stateMachine.rb;
    private float chaseSpeed => stateMachine.GetChaseSpeed();
    private float jumpForce => stateMachine.GetJumpForce();
    private float alturaMaximaAlcancavel => jumpForce * 0.8f;

    public override void Enter()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerSM = playerObject.GetComponent<PlayerStateMachine>();
        }
    }

    public override void Update()
    {
        if (detection.LosePlayer())
        {
            stateMachine.ChangeState(new PatrolState(stateMachine, enemy));
            return;
        }
    }

    public override void FixedUpdate()
    {
        if (player == null) return;

        Vector2 position = rb.position;
        Vector2 targetPosition = new Vector2(player.position.x, position.y);

        float horizontalDistance = Mathf.Abs(targetPosition.x - position.x);

        float targetVelocityX = 0f;
        if (horizontalDistance > 1f)
        {
            float direction = Mathf.Sign(targetPosition.x - position.x);
            targetVelocityX = direction * chaseSpeed;
            Flip();
        }

        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetVelocityX, 0.1f), rb.velocity.y);

        Debug.Log(ShouldJump());

        if (ShouldJump())
        {
            Jump();
        }
    }

    private bool ShouldJump()
    {
        if (playerSM == null) return false;

        float horizontalDistance = Mathf.Abs(player.position.x - stateMachine.transform.position.x);

        if (!playerSM.isGrounded()) return false;

        return PlayerIsAbove() && stateMachine.IsGrounded();
    }

    private bool PlayerIsAbove()
    {
        return player.position.y > stateMachine.transform.position.y + 0.5f;
    }

    private void Flip()
    {
        bool playerRightOfEnemy = player.position.x > stateMachine.transform.position.x;
        if (playerRightOfEnemy && !detection.isFacingRight())
        {
            detection.Flip();
        }
        else if (!playerRightOfEnemy && detection.isFacingRight())
        {
            detection.Flip();
        }
    }

    private void Jump()
    {
        // Removi a verificação do cooldown aqui também

        Debug.Log("PULANDO");
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
