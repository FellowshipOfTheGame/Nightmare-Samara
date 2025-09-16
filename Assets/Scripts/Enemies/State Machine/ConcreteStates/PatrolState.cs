using System.Collections;
using UnityEngine;

public class PatrolState : EnemyState
{
    private float leftBound;
    private float rightBound;
    private bool isFlipping = false;
    private bool collidedThisFrame = false;

    public PatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void Enter()
    {
        Vector2 center = enemy.transform.position;
        leftBound = center.x - enemy.patrolDistance / 2f;
        rightBound = center.x + enemy.patrolDistance / 2f;
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
        //Debug.Log("GROUNDED: " + enemy.isGrounded + " WALLED: " + enemy.isWalled);

        if (!isFlipping)
        {
            enemy.rb.velocity = new Vector2((enemy.facingRight ? 1 : -1) * enemy.patrolSpeed, enemy.rb.velocity.y);

            bool isColliding = !enemy.isGrounded || enemy.isWalled;

            if (isColliding && !collidedThisFrame)
            {
                collidedThisFrame = true;
                enemy.StartCoroutine(FlipCooldown());
                return;
            }

            if (ReachedPatrolDistance())
            {
                enemy.StartCoroutine(FlipCooldown());
                return;
            }
        }
        else
        {
            if (!enemy.isWalled && enemy.isGrounded)
                collidedThisFrame = false;
        }
    }

    private IEnumerator FlipCooldown()
    {
        isFlipping = true;
        enemy.rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(enemy.waitTime);

        enemy.Flip();

        isFlipping = false;
    }

    private bool ReachedPatrolDistance()
    {
        float posX = enemy.transform.position.x;
        return (enemy.facingRight && posX >= rightBound) || (!enemy.facingRight && posX <= leftBound);
    }
}