using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxSpawner : MonoBehaviour
{
    [SerializeField] private SpawnersController spawnersController;
    [SerializeField] private GameObject lootBox;
    [SerializeField] private float spawnRate = 50f;

    // Start is called before the first frame update
    void Start()
    {
        if (lootBox == null) return;

        float range = Random.Range(0, 100);

        if(spawnRate > range && spawnersController.canSpawnLBox())
        {
            Instantiate(lootBox, transform.position, Quaternion.identity);
        }
    }

}
