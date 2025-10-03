using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Usaremos Linq para facilitar a busca do nó com menor distância

[System.Serializable]
public class Graph
{
    public Dictionary<Transform, List<(Transform, float)>> AdjacencyList { get; private set; }

    public Graph(List<Transform> nodes)
    {
        AdjacencyList = new Dictionary<Transform, List<(Transform, float)>>();
        BuildGraph(nodes);
    }

    public void BuildGraph(List<Transform> nodes)
    {
        // ... (o resto do método BuildGraph continua igual)
        AdjacencyList.Clear();

        if (nodes == null) return;

        foreach (var node in nodes)
        {
            if (node != null)
            {
                AdjacencyList[node] = new List<(Transform, float)>();
            }
        }

        var nodeKeys = new List<Transform>(AdjacencyList.Keys);

        for (int i = 0; i < nodeKeys.Count; i++)
        {
            for (int j = i + 1; j < nodeKeys.Count; j++)
            {
                var nodeA = nodeKeys[i];
                var nodeB = nodeKeys[j];

                float distance = Vector2.Distance(nodeA.position, nodeB.position);

                AdjacencyList[nodeA].Add((nodeB, distance));
                AdjacencyList[nodeB].Add((nodeA, distance));
            }
        }
    }
    public Transform FindFarthestNode(Transform startNode)
    {
        if (!AdjacencyList.ContainsKey(startNode))
        {
            return (null); 
        }

        // 1. Inicialização
        var distances = new Dictionary<Transform, float>();
        var unvisitedNodes = new HashSet<Transform>();

        foreach (var node in AdjacencyList.Keys)
        {
            distances[node] = float.MaxValue;
            unvisitedNodes.Add(node);
        }
        distances[startNode] = 0;

        while (unvisitedNodes.Count > 0)
        {
            Transform currentNode = null;
            foreach (var node in unvisitedNodes)
            {
                if (currentNode == null || distances[node] < distances[currentNode])
                {
                    currentNode = node;
                }
            }

            if (distances[currentNode] == float.MaxValue)
            {
                break;
            }

            unvisitedNodes.Remove(currentNode);

            foreach (var (neighbor, weight) in AdjacencyList[currentNode])
            {
                if (unvisitedNodes.Contains(neighbor))
                {
                    float distanceThroughCurrent = distances[currentNode] + weight;
                    if (distanceThroughCurrent < distances[neighbor])
                    {
                        distances[neighbor] = distanceThroughCurrent;
                    }
                }
            }
        }

        float maxDistance = float.MinValue;
        Transform farthestNode = null;

        foreach (var pair in distances)
        {
            if (pair.Value > maxDistance && pair.Value != float.MaxValue)
            {
                maxDistance = pair.Value;
                farthestNode = pair.Key;
            }
        }

        return farthestNode;
    }

    public void DrawGizmos(Color nodeColor, Color edgeColor, float radius = 0.2f)
    {
        if (AdjacencyList == null) return;
        Gizmos.color = edgeColor;
        foreach (var nodeA in AdjacencyList.Keys)
        {
            foreach (var (nodeB, _) in AdjacencyList[nodeA])
            {
                if (nodeA.GetInstanceID() < nodeB.GetInstanceID())
                {
                    Gizmos.DrawLine(nodeA.position, nodeB.position);
                }
            }
        }
        Gizmos.color = nodeColor;
        foreach (var node in AdjacencyList.Keys)
        {
            Gizmos.DrawSphere(node.position, radius);
        }
    }
}