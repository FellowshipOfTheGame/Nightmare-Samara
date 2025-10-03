using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [Header("Debug")]
    [SerializeField] private Color gizmoColor = Color.green;
    [SerializeField] private float gizmoRadius = 0.3f;

    private List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        spawnPoints.Clear();
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
                spawnPoints.Add(t);
        }

        int randomIndex = Random.Range(0, spawnPoints.Count);
        

        GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        playerInstance.transform.localScale = new Vector2(PlayerDirection(spawnPoints[randomIndex]), 1f);
        virtualCamera.Follow = playerInstance.transform;
    }


    private float PlayerDirection(Transform spawnPos)
    {
        RaycastHit2D rightRay = Physics2D.Raycast(spawnPos.position, Vector2.right, 10f);
        RaycastHit2D leftRay = Physics2D.Raycast(spawnPos.position, Vector2.left, 10f);

        if (rightRay.collider != null) {
            return -1f;
        }
        if(leftRay.collider != null) {
            return 1f;
        }

        return 1f;
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
