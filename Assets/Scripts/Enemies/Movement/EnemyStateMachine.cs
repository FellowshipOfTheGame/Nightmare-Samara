using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState currentState;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float edgeCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private void Start()
    {
        ChangeState(new PatrolState(this, gameObject));
    }

    private void Update()
    {
        currentState?.Update();
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public float GetPatrolSpeed() => patrolSpeed;
    public float GetPatrolDistance() => patrolDistance;
    public float GetWaitTime() => waitTime;
    public float GetWallCheckDistance() => wallCheckDistance;
    public float GetEdgeCheckDistance() => edgeCheckDistance;
    public LayerMask GetGroundLayer() => groundLayer;
    public LayerMask GetWallLayer() => wallLayer;


}
