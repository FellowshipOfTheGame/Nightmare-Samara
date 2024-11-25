using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    private SpriteRenderer rend;
    private Color hitColor = Color.red; // Cor de feedback
    private Color originalColor; // Cor original do sprite

    [Header("Invincibility Frames")]
    [SerializeField] private float invincibilityDuration = 1f; // Duração dos iframes
    private bool isInvincible = false; // Controla se o jogador está invulnerável
    private bool isFeedbackRunning = false; // Controla se o feedback visual está rodando

    void Start()
    {
        currentHealth = maxHealth;

        rend = GetComponent<SpriteRenderer>();
        if (rend != null)
        {
            originalColor = rend.color; // Salva a cor original
        }
        else
        {
            Debug.LogError("SpriteRenderer não encontrado!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible || damage <= 0) return; // Ignora dano se estiver invencível ou se for negativo

        currentHealth -= damage;
        Debug.Log("Vida do Personagem: " + currentHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            if (!isFeedbackRunning) // Garantia que o piscar ocorre uma vez
            {
                StartCoroutine(HitFeedback());
            }

            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        if (isInvincible) yield break; // Sai imediatamente se já está invencível

        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private IEnumerator HitFeedback()
    {
        if (rend == null) yield break; // Sai da corrotina se o SpriteRenderer estiver ausente

        isFeedbackRunning = true; // Marca o início do feedback visual

        float elapsedTime = 0f;
        while (elapsedTime < invincibilityDuration)
        {
            // Alterna entre hitColor e originalColor
            rend.color = (rend.color == originalColor) ? hitColor : originalColor;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }

        // Garante que a cor original seja restaurada
        rend.color = originalColor;

        isFeedbackRunning = false; // Marca o fim do feedback visual
    }

    private void GameOver()
    {
        // Rotina de Game Over
        Debug.Log("Game Over");
    }
}
