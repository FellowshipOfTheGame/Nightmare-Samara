using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/States")]
public class EnemyConfig : ScriptableObject
{
    public float groundCheckDistance = 0.5f;

    [Header("Patrol Settings")]
    public float patrolSpeed = 2f;
    public float patrolDistance = 5f;
    public float waitTime = 2f;
    public float wallCheckDistance = 0.5f;
    public float edgeCheckDistance = 0.5f;
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    

    [Header("Chase Settings")]
    public float chaseSpeed = 5f;

}

