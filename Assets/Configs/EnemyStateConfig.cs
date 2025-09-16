using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/States")]
public class EnemyConfig : ScriptableObject
{
    [Header("Patrol Settings")]
    public float patrolSpeed = 2f;
    public float patrolDistance = 5f;
    public float waitTime = 2f;
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    

    [Header("Chase Settings")]
    public float chaseSpeed = 5f;
    public float ledgeCheckDistance = 1f;

}

