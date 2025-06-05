using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    private PlayerState currentState; // Guarda o estado atual do jogador

    [SerializeField] public Rigidbody2D rb; // Rigidbody para fazer operações com física

    [Header("Moving States")]
    [SerializeField] private float walkSpeed = 5f; 
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float acceleration = 100f;

    [Header("Jump State")]
    [SerializeField] private float jumpForce = 7f; //Força do pulo
    [SerializeField] private float fallMultiplier = 2.5f; //Multiplicador da queda, que faz com que a queda aconteça mais rapido que o pulo
    [SerializeField] private float lowJumpMultiplier = 2f; //Multiplicador da subida, que faz com que a subida seja rapida

    [Header("Ground Check")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;

    [Header("Wall Jump")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallSlidingSpeed;


    [Header("Stamina")]
    [SerializeField] private float maxStamina = 10f;
    private float currentStamina;
    [SerializeField] private float idleStaminaGain = 0.01f;
    [SerializeField] private float walkingStaminaGain = 0.1f;
    [SerializeField] private float runningStaminaLoss = 0.3f;
    [SerializeField] private float jumpingStaminaLoss = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Pega o componentes
    }

    private void Start()
    {
        ChangeState(new IdleState(this, gameObject)); //Coloca o estado inicial do player como Idle
        currentStamina = maxStamina;
    }

    private void Update()
    {
        //Debug.Log(rb.velocity.x);
        //Debug.Log(getCurrentStamina());
        currentState?.Update(); //Chama o update para o estado atual
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate(); //Chama o fixedUpdate para o estado atual
    }

    public void ChangeState(PlayerState newState)
    {
        currentState?.Exit(); //Sai do estado anterior
        currentState = newState; //Troca o estado atual
        currentState?.Enter(); // Entra no estado novo
    }

    public void FlipPlayer(float direction)
    {
        //Verifica qual a direção do jogador para flipar ele conforme a direção que ele está andando
        if (direction != 0)
        {
            // Ajusta a escala no eixo X para inverter o sprite
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction); // Direção define o sinal
            transform.localScale = scale;
        }
    }

    //Getters das variaveis que sao usadas em outras classes
    public float getWalkSpeed() => walkSpeed;
    public float getRunSpeed() => runSpeed;
    public float getAcceleration() => acceleration;
    public float getJumpForce() => jumpForce;
    public float getFallMultiplier() => fallMultiplier;
    public float getLowJumpMultiplier() => lowJumpMultiplier;
    public bool isGrounded() {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)) {
            return true;
        }
        else { return false; }
    }

    public bool IsWalled(float direction)
    {
        Vector2 castDirection = Vector2.right * direction;
        Vector2 origin = transform.position;
        Vector2 size = boxSize;
        float distance = castDistance;
        return Physics2D.BoxCast(origin, size, 0, castDirection, distance, wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    public float getCurrentStamina() => currentStamina;

    public bool hasLackOfStamina()
    {
        return currentStamina<maxStamina;
    }

    public void GainStamina(float amount)
    {
        if (currentStamina + amount > maxStamina || currentStamina == maxStamina) { 
            currentStamina = maxStamina;
            return;
        }
        currentStamina += amount;
    }

    public void LossStamina(float amount)
    {
        if (currentStamina - amount < 0 || currentStamina == 0)
        {
            currentStamina = 0;
            return;
        }
        currentStamina -= amount;
    }

    public float getIdleStaminaGain() => idleStaminaGain;
    public float getWalkingStaminaGain() => walkingStaminaGain;
    public float getRunningStaminaLoss() => runningStaminaLoss;
    public float getJumpingStaminaLoss() => jumpingStaminaLoss;

    public float getWallSlideSpeed() => wallSlidingSpeed;

}
