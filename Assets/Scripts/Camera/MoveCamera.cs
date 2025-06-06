using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, -10f);
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private float deadZoneX = 1.5f;
    [SerializeField] private float deadZoneY = 1f;
    [SerializeField] private BoxCollider2D cameraBounds;

    private Vector3 velocity = Vector3.zero;
    private Transform target;

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;

        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 cameraPos = transform.position;
        Vector3 delta = target.position - cameraPos;
        Vector3 newPos = cameraPos;

        if (Mathf.Abs(delta.x) > deadZoneX)
            newPos.x = target.position.x - Mathf.Sign(delta.x) * deadZoneX;

        if (Mathf.Abs(delta.y) > deadZoneY)
            newPos.y = target.position.y - Mathf.Sign(delta.y) * deadZoneY;

        Vector3 targetPos = new Vector3(newPos.x, newPos.y, 0f) + offset;

        // Limita a posição da câmera dentro dos bounds
        Bounds bounds = cameraBounds.bounds;
        float minX = bounds.min.x + camHalfWidth;
        float maxX = bounds.max.x - camHalfWidth;
        float minY = bounds.min.y + camHalfHeight;
        float maxY = bounds.max.y - camHalfHeight;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
