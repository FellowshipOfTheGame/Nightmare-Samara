using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int poisonFlask; //qtd de frascos de veneno
    public int woodenBat; //qtd de tacos de madeira
    public int itemVida;


    void Start()
    {
        poisonFlask = 0;
        woodenBat = 0;
        itemVida = 0;
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //se tocar em algum frasco de veneno coleta ele
        if (collision.gameObject.CompareTag("Veneno"))
        {
            Destroy(collision.gameObject); 
            poisonFlask++; 
            Debug.Log("Frascos de veneno: " + poisonFlask);  
        }

        //se tocar em algum taco de madeira coleta ele
        if (collision.gameObject.CompareTag("Taco"))
        {   
            Destroy(collision.gameObject);  
            woodenBat++;  
            Debug.Log("Tacos de madeira: " + woodenBat);  
        }

        //se tocar em algum taco de madeira coleta ele
        if (collision.gameObject.CompareTag("ItemVida"))
        {   
            Destroy(collision.gameObject);  
            itemVida++;  
            Debug.Log("Item de Vida: " + itemVida);  
        }
    }
}