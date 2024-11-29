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
    [SerializeField] private float invincibilityDuration = 1f; // Dura��o dos iframes
    private bool isInvincible = false; // Controla se o jogador est� invulner�vel

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
            Debug.LogError("SpriteRenderer n�o encontrado!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible || damage <= 0) return; // Ignora dano se estiver invenc�vel ou se for negativo

        currentHealth -= damage;
        Debug.Log("Vida do Personagem: " + currentHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        if (isInvincible) yield break; // Sai imediatamente se j� est� invenc�vel
        if (rend == null) yield break;

        isInvincible = true;
        rend.color = (rend.color == originalColor) ? hitColor : originalColor;
        yield return new WaitForSeconds(invincibilityDuration);
        rend.color = originalColor;
        isInvincible = false;
    }

   

    private void GameOver()
    {
        // Rotina de Game Over
        Debug.Log("Game Over");
    }
}
