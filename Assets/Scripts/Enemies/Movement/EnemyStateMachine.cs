using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState currentState;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EnemyConfig enemyConfig;

    private void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero;

        ChangeState(new PatrolState(this, gameObject, enemyConfig));
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
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        currentState = newState;
        currentState?.Enter();
        Debug.Log("Estado atual: " + currentState.GetType().Name);
    }

}
