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

    private Rigidbody2D rb;

    //Construtor
    public PatrolState(EnemyStateMachine stateMachine, GameObject enemy)
        : base(stateMachine, enemy) { }

    //Define algumas variaveis quando o inimigo entra no estado
    public override void Enter()
    {
        startPosition = enemy.transform.position;
        facingRight = detection?.isFacingRight() ?? true;
        rb = enemy.GetComponent<Rigidbody2D>(); // Garanta a referência
    }

    public override void Update()
    {
        if (detection?.SeesPlayer() == true)
        {
            stateMachine.ChangeState(new ChaseState(stateMachine, enemy));
            return;
        }

        if (isWaiting) return;

        CheckForObstacles();

        // Adicione verificação para evitar chamadas múltiplas
        if ((shouldFlip || ReachedPatrolDistance()) && !isWaiting)
        {
            isWaiting = true;
            stateMachine.StartCoroutine(WaitBeforeFlip()); // Use a máquina de estados
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

        // Ajuste a origem do raycast para evitar auto-detecção
        Vector2 raycastOrigin = pos + new Vector2(0, 0.2f); // Acima do chão

        // Raycast com filtro para ignorar o próprio inimigo
        int layerMask = (wallLayer | groundLayer) & ~(1 << enemy.layer);

        bool hitWall = Physics2D.Raycast(
            raycastOrigin,
            dir,
            wallCheckDistance,
            layerMask
        );

        // Verificação de borda mais precisa
        Vector2 edgeCheckPos = raycastOrigin + (dir * edgeCheckDistance);
        bool onEdge = !Physics2D.Raycast(
            edgeCheckPos,
            Vector2.down,
            0.5f,
            groundLayer
        );

        shouldFlip = hitWall || onEdge;

        // Debug visual
        Debug.DrawRay(raycastOrigin, dir * wallCheckDistance, hitWall ? Color.red : Color.green);
        Debug.DrawRay(edgeCheckPos, Vector2.down * 0.5f, onEdge ? Color.red : Color.blue);
    }

    //Verificação se chegou na distancia maxima de patrulha
    private bool ReachedPatrolDistance() =>
        Vector2.Distance(startPosition, enemy.transform.position) >= patrolDistance;

    //Se move na direção correta
    private void Move()
    {
        float dir = facingRight ? 1f : -1f;
        // Substitua Translate por velocity
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
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
