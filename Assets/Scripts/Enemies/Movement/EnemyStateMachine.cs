using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState currentState;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float edgeCheckDistance = 0.5f;
    [SerializeField] private LayerMask wallLayer;

    [Header("Chase Settings")]
    [SerializeField] private float chaseSpeed = 5f;

    private bool grounded = false; // Guarda se o inimigo esta no chão ou não

    // Define como estado inicial o estado de patrulhamento
    private void Start()
    {
        // Garanta a existência do Rigidbody
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        // Inicialize com velocidade zero
        if (rb != null) rb.velocity = Vector2.zero;

        ChangeState(new PatrolState(this, gameObject));
    }

    //Update do estado em que esta
    private void Update()
    {
        currentState?.Update();
    }

    //FixedUpdate do estado em que esta
    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    //Muda de estado
    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();

        // Reset da física ao trocar estados
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        currentState = newState;
        currentState?.Enter();
        Debug.Log("Estado atual: " + currentState.GetType().Name);
    }

    // Getters
    public float GetPatrolSpeed() => patrolSpeed;
    public float GetPatrolDistance() => patrolDistance;
    public float GetWaitTime() => waitTime;
    public float GetWallCheckDistance() => wallCheckDistance;
    public float GetEdgeCheckDistance() => edgeCheckDistance;
    public LayerMask GetGroundLayer() => groundLayer;
    public LayerMask GetWallLayer() => wallLayer;
    public float GetChaseSpeed() => chaseSpeed;
    public bool IsGrounded() => grounded;

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
