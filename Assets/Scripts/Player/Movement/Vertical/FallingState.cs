using UnityEngine;

public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }

    public override void Update()
    {
        // A HIERARQUIA CORRETA DE VERIFICAÇÃO:

        // 1. PRIORIDADE MÁXIMA: Pousamos no chão?
        if (stateMachine.isGrounded())
        {
            // A queda terminou. AGORA decidimos para qual estado ir
            // com base no input do jogador NO MOMENTO DO POUSO.
            if (isRunning()) // Se o jogador já está segurando as teclas de corrida
            {
                stateMachine.ChangeState(new RunningState(stateMachine, player));
            }
            else if (isWalking()) // Se está segurando só as de movimento
            {
                stateMachine.ChangeState(new WalkingState(stateMachine, player));
            }
            else // Se não está segurando nada
            {
                stateMachine.ChangeState(new IdleState(stateMachine, player));
            }
            return; // Importante para garantir a troca de estado antes de continuar.
        }

        // 2. SEGUNDA PRIORIDADE: Estamos deslizando na parede?
        // (Remova os comentários quando for implementar o WallSlide)
        /*
        else if (isWallSliding()) 
        {
            stateMachine.ChangeState(new WallSlideState(stateMachine, player));
            return;
        }
        */

        // Se nenhuma das condições de saída acima for atendida,
        // o jogador simplesmente continua no estado Falling.
    }

    public override void FixedUpdate()
    {
        // Esta lógica está ótima e deve ser mantida!
        // Ela controla o movimento no ar e a aceleração da gravidade.

        float move = HandleInput();
        stateMachine.FlipPlayer(move);

        // Movimento horizontal durante a queda
        // Usar a velocidade de caminhada como limite no ar é uma boa escolha.
        float maxSpeed = stateMachine.getWalkSpeed();
        float targetX = move * maxSpeed;

        // Uma forma suave de aplicar o controle aéreo
        float newVelocityX = Mathf.Lerp(stateMachine.rb.velocity.x, targetX, Time.fixedDeltaTime * 10f);

        stateMachine.rb.velocity = new Vector2(newVelocityX, stateMachine.rb.velocity.y);

        // Gravidade extra para uma queda mais "pesada" e responsiva
        if (stateMachine.rb.velocity.y < 0f)
        {
            float fallMultiplier = stateMachine.getFallMultiplier();
            stateMachine.rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
        }
    }
}