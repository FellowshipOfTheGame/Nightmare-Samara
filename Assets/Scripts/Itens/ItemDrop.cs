using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Rigidbody2D itemRb;
    public float upForce = 5f;
    public float horizontalForceRange = 1f; //a força aleatoria para a direita ou esquerda

    public GameObject spawnAreaPrefab; //prefab da LootBox
    private BoxCollider2D spawnAreaCollider; //collider da caixa para os objetos nao spawnarem fora da area

    // Start is called before the first frame update
    void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();

        //faz o item ir para cima
        Vector2 force = Vector2.up * upForce;

        //adiciona a força para a direita ou esquerda (escolha aleatoria)
        float horizontalForce = Random.Range(-horizontalForceRange, horizontalForceRange);
        force.x = horizontalForce;

        //aplica a força horizontal
        itemRb.AddForce(force, ForceMode2D.Impulse);
    }
}
