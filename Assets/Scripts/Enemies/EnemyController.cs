using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;
    private int currentHealth;

    [SerializeField] private float delay = 2f;

    [Header("Hit Feedback")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hitColor = Color.red; // Cor ao receber dano
    private Color originalColor; // Cor original do sprite
    [SerializeField] private float hitDuration = 0.2f; // Duração do feedback

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Obtém o SpriteRenderer e salva a cor original
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer não encontrado! O hit feedback não funcionará.");
        }
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        Debug.Log("Vida do inimigo: " + currentHealth);

        // Inicia o feedback visual
        if (spriteRenderer != null)
        {
            StartCoroutine(HitFeedback());
        }

        if (currentHealth <= 0)
        {
            if (gameObject.CompareTag("Skeleton"))
            {
                Disassemble();
            }
            if (gameObject.CompareTag("Rat"))
            {
                Die();
            }
        }
    }

    private IEnumerator HitFeedback()
    {
        // Muda a cor para a cor de hit
        spriteRenderer.color = hitColor;

        // Aguarda o tempo de feedback
        yield return new WaitForSeconds(hitDuration);

        // Restaura a cor original
        spriteRenderer.color = originalColor;
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
