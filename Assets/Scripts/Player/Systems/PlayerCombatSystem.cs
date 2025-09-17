//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCombat : MonoBehaviour
//{
//    [SerializeField] private Transform attackPoint;  //Recebe o transform de um objeto atrelado ao player que representa seu ponto de ataque
//    [SerializeField] private float attackRadius; // Recebe o raio de ataque do player

//    void Update()
//    {
//        //Usa a tecla "Shift Esquerdo" para atacar
//        if(Input.GetKeyDown(KeyCode.F))
//        {
//            Attack();
//        }
//    }

//    private void Attack()
//    {
//        //Encontra todos os objetos no raio de ataque
//        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
//        Collider2D closestEnemyCollider = null;
//        Collider2D closestLootBoxCollider = null;
//        float closestDistance = Mathf.Infinity;

//        //Percorre todos os objetos atingidos
//        foreach (Collider2D collider in hitObjects)
//        {
//            //Verifica se o objeto eh uma LootBox
//            LootBox lootBox = collider.GetComponent<LootBox>();
//            if (lootBox != null)
//            {
//                float distanceToLootBox = Vector2.Distance(attackPoint.position, collider.transform.position);
//                if (distanceToLootBox < closestDistance)
//                {
//                    closestDistance = distanceToLootBox;
//                    closestLootBoxCollider = collider;
//                }
//            }

//            //Verifica se o objeto eh um inimigo
//            Enemy enemy = collider.GetComponent<Enemy>();
//            if (enemy != null)
//            {
//                float distanceToEnemy = Vector2.Distance(attackPoint.position, collider.transform.position);
//                if (distanceToEnemy < closestDistance)
//                {
//                    closestDistance = distanceToEnemy;
//                    closestEnemyCollider = collider;
//                }
//            }
//        }

//        //Se encontrar uma lootbox, aplica o dano
//        if (closestLootBoxCollider != null)
//        {
//            LootBox closestLootBox = closestLootBoxCollider.GetComponent<LootBox>();
//            closestLootBox.TakeDamageBox(); 
//        }

//        //Se encontrar um inimigo proximo, aplica o dano
//        if (closestEnemyCollider != null)
//        {
//            Enemy closestEnemy = closestEnemyCollider.GetComponent<Enemy>();

//            //Se o inimigo proximo for um esqueleto, o ataque sera realizado com um taco de madeira, caso tenha no inventario
//            if (closestEnemyCollider.CompareTag("Skeleton") && playerInventory.getBat() > 0)
//            {
//                playerInventory.useBat();
//                closestEnemy.TakeDamage();
//            }
//            //se o inimigo proximo for um rato, ele só tomará dano com o veneno, caso o player tenha no inventario
//            else if (closestEnemyCollider.CompareTag("Rat") && playerInventory.getPoison() > 0)
//            {
//                playerInventory.usePoison();
//                closestEnemy.TakeDamage();
//            }
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        //Desenha o raio de ataque para ficar visivel no editor
//        if(attackPoint != null)
//        {
//            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
//        }
//    }
//}
