using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject skelleton;
    [SerializeField] private float skelletonSpawnRate = 30f;
    [SerializeField] private GameObject rat;
    [SerializeField] private float ratSpawnRate = 40f;
    [SerializeField] private int max = 10;
    [SerializeField] private Transform enemiesParent;

    [Header("Debug")]
    [SerializeField] private Color gizmoColor = Color.blue;
    [SerializeField] private float gizmoRadius = 0.3f;

    private List<Transform> spawnPoints = new List<Transform>();
    private int aux = 0;

    // Start is called before the first frame update
    void Start()
    {

        if (skelleton == null || rat ==null) return;

        spawnPoints.Clear();
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
                spawnPoints.Add(t);
        }

        for (int i = 0; i < spawnPoints.Count; i++) {
            float range = Random.Range(0, 100);

            if (ratSpawnRate > range && aux < max)
            {
                Instantiate(rat, spawnPoints[i].position, Quaternion.identity, enemiesParent);
                aux++;
            }
            else if (skelletonSpawnRate > range && aux < max)
            {
                Instantiate(skelleton, spawnPoints[i].position, Quaternion.identity, enemiesParent);
                aux++;
            }
        } 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        foreach (Transform t in transform)
        {
            Gizmos.DrawSphere(t.position, gizmoRadius);
        }
    }
}
