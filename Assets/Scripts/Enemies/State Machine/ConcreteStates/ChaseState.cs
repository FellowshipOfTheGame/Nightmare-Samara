using TMPro;
using UnityEngine;

public class ChaseState : EnemyState
{
    private Vector2 startPosition;
    private bool returningToOrigin = false;
    private Transform player;
    private Vector2 nextPos;

    public ChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void Enter()
    {
        startPosition = enemy.transform.position;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        returningToOrigin = false;
    }

    public override void FrameUpdate()
    {
        if (!returningToOrigin && enemy.detection.HasLostPlayer())
        {
            returningToOrigin = true;
        }

        if (returningToOrigin && enemy.detection.SeesPlayer()) {
            returningToOrigin = false;
        }
    }

    public override void PhysicsUpdate()
    {
        if (!returningToOrigin)
        {
            // Verifica se o player ainda existe
            if (player == null)
            {
                returningToOrigin = true;
                return;
            }

            // Determina a direção do player em relação ao inimigo
            float playerDirection = Mathf.Sign(player.position.x - enemy.transform.position.x);

            // Vira o inimigo na direção do player
            if ((playerDirection > 0 && !enemy.facingRight) || (playerDirection < 0 && enemy.facingRight))
            {
                enemy.Flip();
            }

            bool isColliding = !enemy.isGrounded || enemy.isWalled;
            if (!isColliding)
            {
                // Move na direção do player
                enemy.rb.velocity = new Vector2(playerDirection * enemy.chaseSpeed, enemy.rb.velocity.y);
            }
            else
            {
                returningToOrigin = true;
            }
        }
        else
        {
            // Lógica para retornar à posição original
            float directionToOrigin = Mathf.Sign(startPosition.x - enemy.transform.position.x);

            // Vira na direção da posição original
            if ((directionToOrigin > 0 && !enemy.facingRight) || (directionToOrigin < 0 && enemy.facingRight))
            {
                enemy.Flip();
            }

            nextPos = Vector2.MoveTowards(enemy.transform.position, startPosition, enemy.patrolSpeed * Time.deltaTime);
            enemy.rb.MovePosition(nextPos);

            if (Vector2.Distance(enemy.transform.position, startPosition) < 0.1f)
            {
                enemyStateMachine.ChangeState(enemy.patrolState);
                return;
            }
        }
    }
}