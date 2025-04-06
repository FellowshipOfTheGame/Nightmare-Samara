using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 velocity = Vector3.zero;

    private Transform target;

    [Header("Smooth Time")]
    [SerializeField] private float smoothTime = 0.25f;

    [Header("Dead Zone")]
    [SerializeField] private float deadZoneX = 1.5f;
    [SerializeField] private float deadZoneY = 1f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 cameraPos = transform.position - offset; // posição da câmera sem o offset
        Vector3 delta = target.position - cameraPos;     // diferença entre player e centro da câmera

        Vector3 newPos = transform.position;

        // Move no eixo X se sair da dead zone
        if (Mathf.Abs(delta.x) > deadZoneX)
        {
            newPos.x = target.position.x - Mathf.Sign(delta.x) * deadZoneX + offset.x;
        }

        // Move no eixo Y se sair da dead zone
        if (Mathf.Abs(delta.y) > deadZoneY)
        {
            newPos.y = target.position.y - Mathf.Sign(delta.y) * deadZoneY + offset.y;
        }

        // Suaviza o movimento
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(newPos.x, newPos.y, offset.z), ref velocity, smoothTime);
    }
}
