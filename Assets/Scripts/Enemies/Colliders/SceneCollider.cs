using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCollider : MonoBehaviour
{
    private Enemy enemy;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    public float groundRayLenght = 1f;
    public float wallRayLenght = 0.5f;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, groundRayLenght, groundLayer);
        enemy.isGrounded = groundInfo.collider;


        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, Vector2.right, wallRayLenght, wallLayer);
        enemy.isWalled = wallInfo.collider;
    }


}
