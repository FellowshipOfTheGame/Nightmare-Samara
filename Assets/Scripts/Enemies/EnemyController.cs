using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private int maxHealth = 2;
    private int currentHealth;

    [SerializeField] private float delay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    public void TakeDamage()
    {
        currentHealth -= 1;
        Debug.Log("Vida do inimigo: " + currentHealth);

        if (currentHealth <= 0)
        {

            if (gameObject.CompareTag("Skeleton")) {
                Disassemble();
            }
            if (gameObject.CompareTag("Rat"))
            {
                Die();
            }
        }
    }

    private void Disassemble()
    {
        // Faz o GameObject desaparecer
        gameObject.SetActive(false);

        // Reaparece após o tempo especificado
        Invoke("Reappear", delay);
    }

    void Reappear()
    {
        // Faz o esqueleto aparecer novamente
        currentHealth = maxHealth;
        gameObject.SetActive(true);
    }

    private void Die()
    {
        
        Destroy(gameObject); // Remove o inimigo da cena
    }
}
