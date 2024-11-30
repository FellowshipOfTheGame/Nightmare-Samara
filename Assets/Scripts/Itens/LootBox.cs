using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Depois sera implementado e alterado o sistema de cores para dano da caixa:
Talvez seja interessante ter tres artes de caixa, ou seja, uma inteira, uma mais quebrada
e uma completamente quebrada para representar o dano em vez de mudar a cor

No momento, esta vermelho apenas por fins de teste
*/

public class LootBox : MonoBehaviour
{
    [SerializeField] private Color hitColor = Color.red; // Cor ao receber dano
    private Color originalColor; // Cor original do sprite
    [SerializeField] private float hitDuration = 0.2f;

    [SerializeField] public int BoxHealth; //Vida atual da caixa
    [SerializeField] public int Max_BoxHealth; //Vida maxima da caixa
    private SpriteRenderer spriteRenderer;


    public GameObject[] itemDrops;
    [SerializeField] private int venenoDropChance = 80;
    [SerializeField] private int tacoDropChance = 10;
    [SerializeField] private int nothingDropChance = 10;


    // Start is called before the first frame update
    void Start()
    {
        BoxHealth = Max_BoxHealth; //inicialmente, a vida atual da caixa será a maxima

        spriteRenderer = GetComponent<SpriteRenderer>(); 
        if(spriteRenderer != null) {
            originalColor = spriteRenderer.color; //define a cor original da caixa
        } else {
            Debug.LogError("SpriteRenderer não encontrado!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //funcao para a caixa tomar dano
    public void TakeDamageBox() {
        BoxHealth -= 1; //diminui 1 de viida
        Debug.Log("Vida da caixa: " + BoxHealth);

        if(spriteRenderer != null) {
            StartCoroutine(HitFeedback());
        }

        if(BoxHealth <= 0) {
            if(gameObject.CompareTag("LootBox")) {
                Destroy(gameObject);
                for(int i = 0; i < 3; i++) {
                    ItemDrop();
                }
            }
        }
    }

    // sistema para alterar a cor da caixa quando levar dano
    private IEnumerator HitFeedback()
    {
        // Muda a cor para a cor de hit
        spriteRenderer.color = hitColor;

        // Aguarda o tempo de feedback
        yield return new WaitForSeconds(hitDuration);

        // Restaura a cor original
        spriteRenderer.color = originalColor;
    }

    //funcao para dropar itens considerando a probabilidade de cada item ser dropado
    private void ItemDrop() {
        float randomChance = Random.Range(0,100);
        Debug.Log("Valor aleatorio: " + randomChance);

        if(randomChance < venenoDropChance) {
            Instantiate(itemDrops[0], transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        } else if (randomChance < venenoDropChance + tacoDropChance) {
            Instantiate(itemDrops[1], transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        } else if (randomChance < venenoDropChance + tacoDropChance + nothingDropChance){
            Debug.Log("Nenhum item dropado.");
        }
    }
}
