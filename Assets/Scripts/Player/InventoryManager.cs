using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update

    private int poisonFlask;
    private int woodenBat;
    //private bool hasLifeFlask;

    private void Start()
    {
        poisonFlask = 0;    
        woodenBat = 0;
        //hasLifeFlask = false;
    }


    private void ItemRandomizer()
    {
        int randomValue = Random.Range(0, 100);
        if (randomValue < 80)
        {
            // PoisonFlask
            poisonFlask++;
            Debug.Log($"O número de venenos agora é: {poisonFlask}");
        }
        else if (randomValue < 90) // 80 a 89 (10% de chance)
        {
            // WoodenBat
            woodenBat++;
            Debug.Log($"O número de tacos agora é: {woodenBat}");
        }
        else // 90 a 99 (10% de chance)
        {
            // Nothing
            Debug.Log("Nada");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            ItemRandomizer();
            Destroy(collision.gameObject);
        }
    }
}