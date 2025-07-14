using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpawners : MonoBehaviour
{
    private GameObject playerInstance;

    [SerializeField] private GameObject exitPrefab;

    [Header("Debug")]
    [SerializeField] private Color gizmoColor = Color.green;
    [SerializeField] private float gizmoRadius = 0.3f;

    private List<Transform> spawnPoints = new List<Transform>();
    private bool spawnou = false;
    private float maxDistance = float.MinValue;
    private Transform farthestPoint = null;

    void Start()
    {
        spawnPoints.Clear();
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
                spawnPoints.Add(t);
        }
        StartCoroutine(TrySpawnExit());
    }

    IEnumerator TrySpawnExit()
    {
        while (playerInstance == null)
        {
            playerInstance = GameObject.FindWithTag("Player");
            yield return null;
        }

        Transform farthestPoint = GetFarthestSpawnPoint();
        if (farthestPoint != null)
        {
            Instantiate(exitPrefab, farthestPoint.position, Quaternion.identity);
        }
    }

    private Transform GetFarthestSpawnPoint()
    {
        float maxDistance = float.MinValue;
        Transform farthest = null;

        foreach (Transform t in spawnPoints)
        {
            float distance = Vector2.Distance(t.position, playerInstance.transform.position);
            Debug.Log(distance);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                farthest = t;
                
            }
        }

        return farthest;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        foreach (Transform t in transform)
        {
            Gizmos.DrawCube(t.position, new Vector3(gizmoRadius * 3, gizmoRadius *6, 0.1f));
        }
    }
}
