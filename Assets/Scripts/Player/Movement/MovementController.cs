using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public WalkingState walking;
    public JumpingState jumping;
    public GroundedState grounded;

    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public PlayerStates currentXState;
    [HideInInspector] public PlayerStates currentYState;

    [HideInInspector] public bool isGrounded;

    void Start()
    {
        // Começa no estado de andando
        rb = GetComponent<Rigidbody2D>();
        ChangeHorizontalState(walking);
    }

    public void ChangeHorizontalState(PlayerStates newState)
    {
        if (currentXState == newState) return;

        if (currentXState != null) currentXState.Exit();
        currentXState = newState;
        currentXState.Enter();
    }

    public void ChangeVerticalState(PlayerStates newState)
    {
        if (currentYState == newState) return;


        if (currentYState != null) currentYState.Exit();
        currentYState = newState;
        currentYState.Enter();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
