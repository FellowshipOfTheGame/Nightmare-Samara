using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    [SerializeField] private float attackCooldown = 4f; // Intervalo entre os ataques
    [SerializeField] private int damage = 1;               // Quantidade de dano por ataque
    private bool ableToHit = false;

    private Coroutine attackCoroutine;

    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ableToHit = true;

            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack(collision.gameObject.GetComponent<PlayerController>()));
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ableToHit = false;
            if (attackCoroutine != null) // Para a corrotina
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    IEnumerator Attack(PlayerController player)
    {
        while (ableToHit)
        {
            player.TakeDamage(damage);
            yield return new WaitForSeconds(attackCooldown);
        }
    }
}
