using UnityEngine;

public class WallSlideState : PlayerState
{
    private int wallDirection;
    private float slideTimer;
    private bool isFastSliding;
    private bool hasJumped;

    public WallSlideState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }

    public override void Enter()
    {
        wallDirection = stateMachine.GetWallDirection();
        stateMachine.FlipPlayer(-wallDirection); // Olha para o lado oposto da parede
        slideTimer = 0f;
        isFastSliding = false;
        hasJumped = false;
    }

    public override void Update()
    {
        slideTimer += Time.deltaTime;

        // Verifica se o jogador pode pular
        if (!hasJumped && isJumping())
        {
            hasJumped = true;
            stateMachine.ChangeState(new WallJumpState(stateMachine, player));
            return;
        }

        // Verifica se o jogador ainda está na parede
        if (!stateMachine.IsWalled(wallDirection))
        {
            stateMachine.ChangeState(new FallingState(stateMachine, player));
            return;
        }

        // Verifica se o jogador tocou o chão
        if (stateMachine.isGrounded())
        {
            stateMachine.ChangeState(new IdleState(stateMachine, player));
            return;
        }

        // Verifica se o jogador quer descer mais rápido
        isFastSliding = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
    }

    public override void FixedUpdate()
    {
        Vector2 vel = stateMachine.rb.velocity;

        // Define a velocidade de deslize
        float slideSpeed = isFastSliding ?
            stateMachine.getWallSlideSpeed() * 1.8f :
            stateMachine.getWallSlideSpeed();

        // Mantém o jogador colado na parede enquanto desliza
        vel.x = wallDirection * 0.1f;

        // Controla a velocidade de descida
        vel.y = Mathf.Clamp(vel.y, -slideSpeed, float.MaxValue);

        stateMachine.rb.velocity = vel;

        // Aplica pequeno impulso para manter contato com a parede
        stateMachine.rb.AddForce(new Vector2(wallDirection * 0.5f, 0), ForceMode2D.Impulse);
    }

    public override void Exit()
    {
        // Pequeno impulso ao sair da parede
        if (!hasJumped)
        {
            stateMachine.rb.AddForce(new Vector2(-wallDirection * 1f, 0.5f), ForceMode2D.Impulse);
        }
    }
}