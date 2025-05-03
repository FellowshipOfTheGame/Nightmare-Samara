using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private int poisonFlask; //Quantidade de 
    private int woodenBat; //qtd de tacos de madeira
    //private int itemVida;


    void Start()
    {
        poisonFlask = 0;
        woodenBat = 0;
        //itemVida = 0;
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

        /*
        //se tocar em algum taco de madeira coleta ele
        if (collision.gameObject.CompareTag("ItemVida"))
        {   
            Destroy(collision.gameObject);  
            itemVida++;  
            Debug.Log("Item de Vida: " + itemVida);  
        }
        */
    }

    public int getPoison()
    {
        return poisonFlask;
    }

    public void usePoison()
    {
        this.poisonFlask--;
    }


    public int getBat()
    {
        return woodenBat;
    }

    public void useBat()
    {
        this.poisonFlask--;
    }

    /*
    public int getVida()
    {
        return itemVida;
    }
    */
}