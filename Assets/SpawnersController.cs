using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour
{

    private int lootBoxCount = 0;
    [SerializeField] private int maxLootBoxes = 10;

    private int enemyCount = 0;
    [SerializeField] private int maxEnemies = 10;

    public void addLootBox()
    {
        this.lootBoxCount++;
    }

    public void addEnemy()
    {
        this.enemyCount++;
    }

    public bool canSpawnEnemy()
    {
        if(enemyCount >= maxEnemies)
        {
            return false;
        }
        return true;
    }

    public bool canSpawnLBox()
    {
        if (lootBoxCount >= maxLootBoxes)
        {
            return false;
        }
        return true;
    }
}
