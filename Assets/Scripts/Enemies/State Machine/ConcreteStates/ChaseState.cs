using UnityEngine;

public class ChaseState : EnemyState
{

    private Vector2 startPosition;
    private bool returningToOrigin = false;

    private Transform player;

    public ChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void Enter()
    {
        startPosition = enemy.transform.position;
        enemy.rb = enemy.GetComponent<Rigidbody2D>();

        returningToOrigin = false;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public override void FrameUpdate()
    {
        if (!returningToOrigin && enemy.detection.HasLostPlayer())
        {
            returningToOrigin = true;
        }
    }

    public override void PhysicsUpdate()
    {
        bool foundLedge = enemy.isWalled || !enemy.isGrounded;
        
        if (player == null) return;

        if (returningToOrigin)
        {
            ReturnToOrigin(foundLedge);
            return;
        }

        if (player == null) return;

        ChasePlayer();
    }

    private void ReturnToOrigin(bool foundLedge)
    {
        // Voltar à posição inicial
        Vector2 currentPosition = enemy.rb.position;
        float distance = Vector2.Distance(currentPosition, startPosition);

        if (distance < 0.1f)
        {
            enemy.rb.velocity = Vector2.zero;
            enemyStateMachine.ChangeState(enemy.patrolState);
            return;
        }

        Vector2 direction = (startPosition - currentPosition).normalized;
        float returnSpeed = enemy.chaseSpeed * 0.75f;

        // Verifica se há obstáculos durante o retorno
        if (foundLedge)
        {
            FlipTowards(startPosition);
            enemy.rb.velocity = new Vector2(0f, enemy.rb.velocity.y);
        }
        else
        {
            enemy.rb.velocity = new Vector2(direction.x * returnSpeed, enemy.rb.velocity.y);
            FlipTowards(startPosition);
        }


        Debug.DrawLine(currentPosition, startPosition, Color.green);
    }

    private void ChasePlayer()
    {
        Vector2 position = enemy.rb.position;
        Vector2 targetPosition = new Vector2(player.position.x, position.y);

        float horizontalDistance = Mathf.Abs(targetPosition.x - position.x);

        // Verifica se há obstáculos antes de se mover
        if ((enemy.isWalled) && horizontalDistance > 1f)
        {
            // Para o movimento se encontrar obstáculo
            enemy.rb.velocity = new Vector2(0, enemy.rb.velocity.y);
            return;
        }

        float targetVelocityX = 0f;
        if (horizontalDistance > 1f)
        {
            float direction = Mathf.Sign(targetPosition.x - position.x);
            targetVelocityX = direction * enemy.chaseSpeed;
            Flip();
        }

        enemy.rb.velocity = new Vector2(Mathf.Lerp(enemy.rb.velocity.x, targetVelocityX, 0.1f), enemy.rb.velocity.y);
    }

    private void Flip()
    {
        bool playerRightOfEnemy = player.position.x > enemy.transform.position.x;
        if (playerRightOfEnemy && !enemy.facingRight)
        {
            enemy.Flip();
        }
        else if (!playerRightOfEnemy && enemy.facingRight)
        {
            enemy.Flip();
        }
    }

    private void FlipTowards(Vector2 target)
    {
        bool targetRight = target.x > enemy.transform.position.x;
        if (targetRight != enemy.facingRight)
        {
            enemy.Flip();
        }
    }
}
