using System.Collections;
using UnityEngine;

public class PatrolState : EnemyState
{
    private float leftBound;
    private float rightBound;
    private bool isFlipping = false;

    public PatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void Enter()
    {
        ResetPatrolBounds();
    }

    public override void FrameUpdate()
    {
        if (enemy.detection.SeesPlayer())
        {
            enemyStateMachine.ChangeState(enemy.chaseState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        bool grounded = enemy.isGrounded;
        bool walled = enemy.isWalled;
        bool reachedPatrol = ReachedPatrolDistance();

        if (!isFlipping)
        {
            if (walled || !grounded || reachedPatrol)
            {
                enemy.StartCoroutine(FlipCooldown());
            }
            else
            {
                Move();
            }
        }
    }

    private IEnumerator FlipCooldown()
    {
        isFlipping = true;
        enemy.rb.velocity = Vector2.zero;

        // Reduzi o tempo de espera para teste
        yield return new WaitForSeconds(0.2f);

        enemy.Flip();

        // Só recalcula bounds se foi por obstáculo
        if (enemy.isWalled || !enemy.isGrounded)
        {
            ResetPatrolBounds();
        }

        // Pequeno delay extra para garantir que os colliders atualizem
        yield return new WaitForSeconds(0.05f);

        isFlipping = false;
    }

    // 🔥 NOVO MÉTODO: Redefine os limites de patrulha
    private void ResetPatrolBounds()
    {
        Vector2 center = enemy.transform.position;
        leftBound = center.x - enemy.patrolDistance / 2f;
        rightBound = center.x + enemy.patrolDistance / 2f;

        Debug.Log($"Novos limites: Left={leftBound}, Right={rightBound}");
    }

    private bool ReachedPatrolDistance()
    {
        float posX = enemy.transform.position.x;
        return (enemy.facingRight && posX >= rightBound) || (!enemy.facingRight && posX <= leftBound);
    }

    private void Move()
    {
        float dir = enemy.facingRight ? 1f : -1f;
        enemy.rb.velocity = new Vector2(dir * enemy.patrolSpeed, enemy.rb.velocity.y);
        Debug.Log("se movendo");
    }
}