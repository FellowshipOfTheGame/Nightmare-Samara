using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ExitSpawners : MonoBehaviour
{
    [SerializeField] private GameObject exitPrefab;

    [Header("Gizmos")]
    [SerializeField] private bool showGraphGizmos = true;
    [SerializeField] private Color nodeColor = Color.yellow;
    [SerializeField] private Color edgeColor = Color.green;
    [SerializeField] private float gizmoRadius = 0.25f;

    private GameObject exitInstance;
    private List<Transform> spawnPoints = new List<Transform>();
    private Graph graph;
    private Transform player;

    void Start()
    {
        CollectSpawnPoints();
        graph = new Graph(spawnPoints);
        StartCoroutine(TrySpawnExit());
        player = GetComponent<Transform>();
    }

    private void CollectSpawnPoints()
    {
        spawnPoints.Clear();
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform) spawnPoints.Add(t);
        }
    }

    private IEnumerator TrySpawnExit()
    {

        GameObject playerObject = null;
        while (playerObject == null)
        {
            playerObject = GameObject.FindWithTag("Player");
            yield return null;
        }

        Transform startNode = FindNodeClosestTo(playerObject.transform);
        if (startNode == null)
        {
            Debug.LogError("Não foi possível encontrar um nó de partida no grafo.");
            yield break;
        }

        Transform farthestPoint = graph.FindFarthestNode(startNode);

        if (farthestPoint != null)
        {
            Instantiate(exitPrefab, farthestPoint.position, Quaternion.identity);
            Debug.Log($"Saída criada em '{farthestPoint.name}'");
        }
    }


    private Transform FindNodeClosestTo(Transform target)
    {
        if (target == null || spawnPoints.Count == 0) return null;

        float minDistance = float.MaxValue;
        Transform closestNode = null;

        foreach (Transform node in spawnPoints)
        {
            if (node == null) continue;

            float dist = Vector2.Distance(node.position, target.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestNode = node;
            }
        }
        return closestNode;
    }

    private void OnDrawGizmos()
    {
        if (!showGraphGizmos) return;

 
        if (graph == null || graph.AdjacencyList.Count != (transform.childCount))
        {
            CollectSpawnPoints();
            graph = new Graph(spawnPoints);
        }

        // Apenas desenha o grafo
        graph.DrawGizmos(nodeColor, edgeColor, gizmoRadius);
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            CollectSpawnPoints();
            graph = new Graph(spawnPoints);
        }
    }
}