using UnityEngine;

public class WallJumpState : PlayerState
{
    private float wallJumpDuration = 0.2f;
    private float timer = 0f;

    public WallJumpState(PlayerStateMachine stateMachine, GameObject player) : base(stateMachine, player) { }

    public override void Enter()
    {
        timer = wallJumpDuration;

        // Detecta se o jogador está encostando em uma parede à direita ou à esquerda
        int wallDir = 0;
        if (stateMachine.IsWalled(1f)) wallDir = 1;   // parede à direita
        else if (stateMachine.IsWalled(-1f)) wallDir = -1;  // parede à esquerda

        // Direção do pulo será para o lado oposto da parede
        int jumpDir = -wallDir;

        // Aplica o impulso de wall jump
        Vector2 force = new Vector2(jumpDir * stateMachine.getwallJumpingPower().x, stateMachine.getwallJumpingPower().y);
        stateMachine.rb.velocity = Vector2.zero;
        stateMachine.rb.AddForce(force, ForceMode2D.Impulse);

        // Gasta estamina
        //stateMachine.LossStamina(stateMachine.getJumpingStaminaLoss());

        // Flipa o personagem na direção do pulo
        stateMachine.FlipPlayer(jumpDir);
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        // Depois do tempo de impulso, volta ao JumpingState
        if (timer <= 0f)
        {
            stateMachine.ChangeState(new JumpingState(stateMachine, player));
        }
    }

    public override void FixedUpdate()
    {
        // Durante a duração do pulo na parede, o controle do jogador pode ser travado
    }
}
 