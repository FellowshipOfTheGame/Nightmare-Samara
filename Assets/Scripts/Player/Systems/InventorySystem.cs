using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    Player player;

    public InventorySystem(Player player)
    {
        this.player = player;
        player.itemIndex = 0;
    }

    public void verificaItem()
    {
        if (player.itemIndex == 0) {
            Debug.Log("Trocou para mão");
        } else if (player.itemIndex == 1)
        {
            Debug.Log("Trocou para bastao");
        }
        if (player.itemIndex == 2)
        {
            Debug.Log("Trocou para veneno");
        }

    }
    public void goToNext() {
        if (player.itemIndex + 1 < 3) {
            player.itemIndex++;
        }
        else
        {
            player.itemIndex = 0;
        }
        verificaItem();
    }

    public void goToLast()
    {
        if (player.itemIndex - 1 >= 0)
        {
            player.itemIndex--;
        }
        else
        {
            player.itemIndex = 2;
        }
        verificaItem();
    }

    public void goToIndex(int index) { 
        player.itemIndex = index; 
    }

}
