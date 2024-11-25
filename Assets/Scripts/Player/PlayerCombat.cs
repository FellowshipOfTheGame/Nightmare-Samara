using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
        Collider2D closestEnemyCollider = null;
        float closestDistance = Mathf.Infinity;

        // Encontra o inimigo mais próximo
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            EnemyController enemy = enemyCollider.GetComponent<EnemyController>();
            if (enemy != null)
            {
                float distanceToEnemy = Vector2.Distance(attackPoint.position, enemyCollider.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemyCollider = enemyCollider;
                }
            }
        }

        // Aplica dano ao inimigo mais próximo, se encontrado
        if (closestEnemyCollider != null)
        {
            EnemyController closestEnemy = closestEnemyCollider.GetComponent<EnemyController>();
            InventoryManager playerInventory = gameObject.GetComponent<InventoryManager>();

            if (closestEnemyCollider.CompareTag("Skeleton") && playerInventory.woodenBat > 0)
            {
                //Hit no Esqueleto
                playerInventory.woodenBat--;
                closestEnemy.TakeDamage();
            }
            if (closestEnemyCollider.CompareTag("Rat") && playerInventory.poisonFlask > 0)
            {
                //Hit no rato
                playerInventory.poisonFlask--;
                closestEnemy.TakeDamage();
            }

            
        }
    }



    private void OnDrawGizmos()
    {
        if(attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}
