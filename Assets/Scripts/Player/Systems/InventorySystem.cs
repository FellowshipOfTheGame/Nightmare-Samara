using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    Player player;
    List<int> inventory = new List<int>();

    public InventorySystem(Player player)
    {
        this.player = player;
        inventory.Add(0);
        inventory.Add(1);   
        inventory.Add(2);
        player.itemIndex = 0;
    }

    public void goToNext() {
        if (player.itemIndex + 1 < inventory.Count) {
            player.itemIndex++;
        }
        else
        {
            player.itemIndex = 0;
        }
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
    }

    public void goToIndex(int index) { 
        player.itemIndex = index; 
    }

}
