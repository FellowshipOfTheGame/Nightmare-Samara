using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    private Vector2 startPosition;
    private bool isFlipping = false;
    private bool facingRight;

    private Rigidbody2D rb;

    public PatrolState(EnemyStateMachine stateMachine, GameObject enemy, EnemyConfig enemyConfig)
        : base(stateMachine, enemy, enemyConfig) { }

    public override void Enter()
    {
        startPosition = enemy.transform.position;
        facingRight = detection?.isFacingRight() ?? true;
        rb = enemy.GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdate()
    {
        if (isFlipping) return;

        // Se atingir parede ou borda, vira
        if (collisions.isWalled || !collisions.isGrounded || ReachedPatrolDistance())
        {
            stateMachine.StartCoroutine(FlipCooldown());
            return;
        }

        // Andar normalmente
        Move();
    }

    private IEnumerator FlipCooldown()
    {
        isFlipping = true;

        // Para o inimigo antes de virar
        rb.velocity = Vector2.zero;

        // Espera um pouco
        yield return new WaitForSeconds(enemyConfig.waitTime);

        // Vira de direção
        facingRight = !facingRight;
        detection.Flip();

        // Atualiza o ponto inicial para contar a próxima distância
        startPosition = enemy.transform.position;

        isFlipping = false;
    }

    private bool ReachedPatrolDistance() =>
        Vector2.Distance(startPosition, enemy.transform.position) >= enemyConfig.patrolDistance;

    private void Move()
    {
        float dir = facingRight ? 1f : -1f;
        rb.velocity = new Vector2(dir * enemyConfig.patrolSpeed, rb.velocity.y);
        Debug.Log("CHEGOU NO MOVE");
    }
}
