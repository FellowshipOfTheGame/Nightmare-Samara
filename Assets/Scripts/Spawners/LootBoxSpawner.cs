using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxSpawner : MonoBehaviour
{
    private SpawnersController spawnersController;
    [SerializeField] private GameObject lootBox;
    [SerializeField] private float spawnRate = 50f;

    // Start is called before the first frame update
    void Start()
    {
        spawnersController = GameObject.Find("SpawnersController").GetComponent<SpawnersController>();

        if (lootBox == null) return;

        float range = Random.Range(0, 100);

        Debug.Log(spawnRate > range);
        if(spawnRate > range && spawnersController.canSpawnLootBox())
        {
            Instantiate(lootBox, transform.position, Quaternion.identity);
            spawnersController.addLootBox();
        }
    }

}
