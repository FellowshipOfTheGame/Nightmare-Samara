using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject lootBox;
    [SerializeField] private float spawnRate = 50f;
    [SerializeField] private int max = 10;
    [SerializeField] private Transform lootBoxesParent;

    [Header("Debug")]
    [SerializeField] private Color gizmoColor = Color.green;
    [SerializeField] private float gizmoRadius = 0.3f;

    private List<Transform> spawnPoints = new List<Transform>();
    private int aux = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (lootBox == null) return;

        spawnPoints.Clear();
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
                spawnPoints.Add(t);
        }

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            float range = Random.Range(0, 100);

            if (spawnRate > range && aux < max)
            {
                Instantiate(lootBox, spawnPoints[i].position, Quaternion.identity, lootBoxesParent);
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
