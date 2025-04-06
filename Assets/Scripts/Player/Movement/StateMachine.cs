using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Dictionary<System.Type, PlayerStates> states;

    [HideInInspector] public PlayerStates currentXState;
    [HideInInspector] public PlayerStates currentYState;

    private bool isGrounded;
    private float moveInput;

    void Start()
    { 
        ChangeHorizontalState<IdleState>();
        ChangeVerticalState<GroundedState>();
    }

    private void Awake()
    {
        states = new Dictionary<System.Type, PlayerStates>
        {
            { typeof(IdleState), GetComponent<IdleState>() },
            { typeof(WalkingState), GetComponent<WalkingState>() },
            { typeof(RunningState), GetComponent<RunningState>() },
            { typeof(JumpingState), GetComponent<JumpingState>() },
            { typeof(GroundedState), GetComponent<GroundedState>() }
        };
    }


    public void ChangeHorizontalState<T>() where T : PlayerStates
    {
        var newState = states[typeof(T)];

        if (currentXState == newState) return;

        currentXState?.Exit();
        currentXState = newState;
        currentXState.Enter();
    }

    public void ChangeVerticalState<T>() where T : PlayerStates
    {
        var newState = states[typeof(T)];

        if (currentYState == newState) return;

        currentYState?.Exit();
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

    public bool Grounded() => isGrounded;
    public float getMoveInput() => moveInput;

    public void setMoveInput(float value) { moveInput = value; }
}
