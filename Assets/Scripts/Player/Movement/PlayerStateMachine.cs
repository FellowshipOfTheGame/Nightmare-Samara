using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    private PlayerState currentState;

    [SerializeField] public Rigidbody2D rb;

    [Header("Walk State")]
    [SerializeField] private float walkSpeed = 5f;
    //[SerializeField] private float runSpeed = 10f;

    [Header("Jump State")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float fallMultiplier = 2.5f;

    private bool grounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ChangeState(new IdleState(this, gameObject));
    }

    private void Update()
    {
        currentState?.Update();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void FlipPlayer(float direction)
    {
        if (direction != 0)
        {
            // Ajusta a escala no eixo X para inverter o sprite
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction); // Direção define o sinal
            transform.localScale = scale;
        }
    }

    public float getWalkSpeed() => walkSpeed;
    //public float getRunSpeed() => runSpeed;

    public float getJumpForce() => jumpForce;
    public float getFallMultiplier() => fallMultiplier;

    public bool isGrounded() => grounded;

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
