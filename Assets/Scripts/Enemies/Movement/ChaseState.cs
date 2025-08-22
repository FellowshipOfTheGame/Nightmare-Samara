//using UnityEngine;

//public class ChaseState : EnemyState
//{
//    private UnityEngine.Transform player;
//    private PlayerStateMachine playerSM;

//    public ChaseState(EnemyStateMachine stateMachine, GameObject enemy)
//     : base(stateMachine, enemy) { }

//    private Rigidbody2D rb => stateMachine.rb;
//    private float chaseSpeed => stateMachine.GetChaseSpeed();
//    private float edgeCheckDistance => stateMachine.GetEdgeCheckDistance();
//    private LayerMask groundLayer => stateMachine.GetGroundLayer();

//    private Vector2 initialPosition;
//    private bool returningToOrigin = false;

//    public override void Enter()
//    {
//        initialPosition = stateMachine.transform.position;
//        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
//        if (playerObject != null)
//        {
//            player = playerObject.transform;
//            playerSM = playerObject.GetComponent<PlayerStateMachine>();
//        }
//    }

//    public override void Update()
//    {
//        if (!returningToOrigin && detection.LosePlayer())
//        {
//            stateMachine.ChangeState(new PatrolState(stateMachine, enemy));
//            return;
//        }
//    }

//    public override void FixedUpdate()
//    {
//        if (player == null) return;

//        if (returningToOrigin)
//        {
//            // Voltar à posição inicial
//            Vector2 currentPosition = rb.position;
//            float distance = Vector2.Distance(currentPosition, initialPosition);

//            if (distance < 0.1f)
//            {
//                rb.velocity = Vector2.zero;
//                stateMachine.ChangeState(new PatrolState(stateMachine, enemy));
//                return;
//            }

//            Vector2 direction = (initialPosition - currentPosition).normalized;
//            float returnSpeed = chaseSpeed * 0.75f;
//            rb.velocity = new Vector2(direction.x * returnSpeed, rb.velocity.y);

//            FlipTowards(initialPosition);
//            return;
//        }

//        Vector2 position = rb.position;
//        Vector2 targetPosition = new Vector2(player.position.x, position.y);

//        float horizontalDistance = Mathf.Abs(targetPosition.x - position.x);

//        float targetVelocityX = 0f;
//        if (horizontalDistance > 1f && !IsNearLedge())
//        {
//            float direction = Mathf.Sign(targetPosition.x - position.x);
//            targetVelocityX = direction * chaseSpeed;
//            Flip();
//        }

//        if (IsNearLedge())
//        {
//            returningToOrigin = true;
//            return;
//        }

//        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetVelocityX, 0.1f), rb.velocity.y);
        
//    }

//    private bool IsNearLedge()
//    {
//        Vector2 dir = detection.isFacingRight() ? Vector2.right : Vector2.left;
//        Vector2 edgeCheckOrigin = (Vector2)enemy.transform.position + (dir * edgeCheckDistance);

//        // Faz um raycast vertical para baixo a partir da "beirada"
//        bool groundDetected = Physics2D.Raycast(edgeCheckOrigin, Vector2.down, 0.1f, groundLayer);

//        Debug.DrawRay(edgeCheckOrigin, Vector2.down * 0.1f, Color.red); // Para visualização no editor

//        return !groundDetected; // Se não detectou chão, está na borda
//    }


//    private void Flip()
//    {
//        bool playerRightOfEnemy = player.position.x > stateMachine.transform.position.x;
//        if (playerRightOfEnemy && !detection.isFacingRight())
//        {
//            detection.Flip();
//        }
//        else if (!playerRightOfEnemy && detection.isFacingRight())
//        {
//            detection.Flip();
//        }
//    }

//    private void FlipTowards(Vector2 target)
//    {
//        bool targetRight = target.x > stateMachine.transform.position.x;
//        if (targetRight != detection.isFacingRight())
//        {
//            detection.Flip();
//        }
//    }
//}
