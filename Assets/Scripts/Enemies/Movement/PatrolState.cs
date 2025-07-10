using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    private Vector2 startPosition;
    private bool isWaiting = false;
    private bool shouldFlip = false;
    private bool facingRight;

    //Pega as variaveis do stateMachine
    private float speed => stateMachine.GetPatrolSpeed();
    private float patrolDistance => stateMachine.GetPatrolDistance();
    private float waitTime => stateMachine.GetWaitTime();
    private float wallCheckDistance => stateMachine.GetWallCheckDistance();
    private float edgeCheckDistance => stateMachine.GetEdgeCheckDistance();
    private LayerMask groundLayer => stateMachine.GetGroundLayer();
    private LayerMask wallLayer => stateMachine.GetWallLayer();

    //Construtor
    public PatrolState(EnemyStateMachine stateMachine, GameObject enemy)
        : base(stateMachine, enemy) { }

    //Define algumas variaveis quando o inimigo entra no estado
    public override void Enter()
    {
        startPosition = enemy.transform.position;
        facingRight = detection?.isFacingRight() ?? true;
    }

    public override void Update()
    {   
        //Transiciona de estado para o estado de perseguição caso tenha enxergado o jogador
        if (detection?.SeesPlayer() == true)
        {
            stateMachine.ChangeState(new ChaseState(stateMachine, enemy));
            return;
        }

        if (isWaiting) return;

        CheckForObstacles(); // Verifica se tem obstaculos na frente

        //Verifica se chegou na distancia maxima de patrulha e chama uma coroutina para esperar um pouco antes de flipar o inimigo e continuar
        if (shouldFlip || ReachedPatrolDistance())
        {
            enemy.GetComponent<EnemyStateMachine>().StartCoroutine(WaitBeforeFlip());
        }
        else
        {
            Move();
        }
    }

    //Cria dois raycasts para verificar se o inimigo esta perto de uma parede ou esta perto de uma borda para impedir que ele trave na parede ou se jogue da borda
    private void CheckForObstacles()
    {
        Vector2 dir = facingRight ? Vector2.right : Vector2.left;
        Vector2 pos = enemy.transform.position;

        bool hitWall = Physics2D.Raycast(pos, dir, wallCheckDistance, wallLayer);
        bool onEdge = !Physics2D.Raycast(pos + (Vector2.down * 0.5f) + (dir * edgeCheckDistance), Vector2.down, 0.1f, groundLayer);

        shouldFlip = hitWall || onEdge;
    }

    //Verificação se chegou na distancia maxima de patrulha
    private bool ReachedPatrolDistance() =>
        Vector2.Distance(startPosition, enemy.transform.position) >= patrolDistance;

    //Se move na direção correta
    private void Move()
    {
        float dir = facingRight ? 1f : -1f;
        enemy.transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
    }

    //Espera um tempo antes de virar o inimigo e continuar o movimento para o outro lado
    private IEnumerator WaitBeforeFlip()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        facingRight = !facingRight;
        detection?.Flip();
        startPosition = enemy.transform.position;
        shouldFlip = false;
        isWaiting = false;
    }
}
