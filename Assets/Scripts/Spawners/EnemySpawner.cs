using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private SpawnersController spawnersController;
    [SerializeField] private GameObject skelleton;
    [SerializeField] private float skelletonSpawnRate = 30f;
    [SerializeField] private GameObject rat;
    [SerializeField] private float ratSpawnRate = 40f;
    [SerializeField] private float nothingSpawnRate = 30f;


    // Start is called before the first frame update
    void Start()
    {
        spawnersController = GameObject.Find("SpawnersController").GetComponent<SpawnersController>();

        if (skelleton == null || rat ==null) return;

        float range = Random.Range(0, 100);
        /*
        Debug.Log(spawnRate > range);
        if (spawnRate > range && spawnersController.canSpawnLootBox())
        {
            Instantiate(lootBox, transform.position, Quaternion.identity);
            spawnersController.addLootBox();
        }
        */
    }
}
