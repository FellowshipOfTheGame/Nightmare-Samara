using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    //bool hasWeapon = false;
    //bool hasRange = false;


    private void Update()
    {
        /*
        if (hasWeapon)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && hasRange)
            {
                Debug.Log("BATEU");
            }
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            //hasWeapon = true;
            Destroy(collision.gameObject);
        }
    }
}