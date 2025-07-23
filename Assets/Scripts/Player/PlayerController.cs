using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 5; //Vida maxima  
    private int currentHealth; //Vida atual

    private SpriteRenderer rend; //Rend para trocar a renderização do player
    private Color hitColor = Color.red; //Cor para representar que esta invencivel
    private Color originalColor; //Cor padrão do player

    [Header("Invincibility Frames")]
    [SerializeField] private float invincibilityDuration = 1f; //Duração da invencibilidade
    private bool isInvincible = false; //Variavel que guarda se esta invencivel ou não

    void Start()
    {
        currentHealth = maxHealth; //Seta a vida atual como a vida máxima

        rend = GetComponent<SpriteRenderer>(); //Pega o componente sprite renderer do objeto

        /*
         * Verifica se o rend esta nulo ou nao
         * Se não ele guarda a cor original do objeto
         * Se sim ele retorna um erro no console
         */
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
        /*Verifica se esta invencivel ou se a vida atual é menor ou igual a 0 para retornar da função e nao realizar o dano no player */
        if (isInvincible || currentHealth <= 0)
        {
            return;   
        }
        
        currentHealth -= damage; // Retira o dano na vida atual
        Debug.Log("Vida do Personagem: " + currentHealth); //Linha para debug da vida atual

        /*
         * Verifica se a vida atual é menor ou igual a 0
         * Se sim ele chama o metodo de game over
         * Se nao ele chama a Coroutine que aplica os frames de invencibilidade no player
         */
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
        /*Verifica se ele ja esta invencivel ou se o rend do player nao é nulo para sair imediatamente da rotina*/
        if (isInvincible) yield break; 
        if (rend == null) yield break;

        isInvincible = true; //Seta o estado de invencivel como false
        /*Verifica se o player esta sendo renderizado na cor original, para ver se ela se mantem ou se ela pode trocar*/
        rend.color = (rend.color == originalColor) ? hitColor : originalColor; 
        yield return new WaitForSeconds(invincibilityDuration); //Espera os segundos passados na variavel
        rend.color = originalColor; // Retorna a cor para original
        isInvincible = false; // Seta o estado de invencivel como false
    }

    private void GameOver()
    {
        //Metodo de game over que por enquanto só destroi o objeto do player
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }
}
