using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    private PlayerState currentState; // Guarda o estado atual do jogador

    [SerializeField] public Rigidbody2D rb; // Rigidbody para fazer operações com física

    [Header("Moving States")]
    [SerializeField] private float walkSpeed = 5f; //Velocidade ao andar
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float acceleration = 100f;

    [Header("Jump State")]
    [SerializeField] private float jumpForce = 7f; //Força do pulo
    [SerializeField] private float fallMultiplier = 2.5f; //Multiplicador da queda, que faz com que a queda aconteça mais rapido que o pulo
    [SerializeField] private float lowJumpMultiplier = 2f; //Multiplicador da subida, que faz com que a subida seja rapida

    private bool grounded = false; // Guarda se o player esta no chão ou não

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Pega o componentes
    }

    private void Start()
    {
        ChangeState(new IdleState(this, gameObject)); //Coloca o estado inicial do player como Idle
    }

    private void Update()
    {
        Debug.Log(rb.velocity.x);
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

    public bool isGrounded() => grounded;

    /*Verifica a entrada na colisao com o chao e a saida para mudar a variavel grounded*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

}
