using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public BehaviorGraphAgent behaviorGraph { get; private set; }

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isWalled;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        behaviorGraph = GetComponent<BehaviorGraphAgent>();
    }

    private void Start()
    {
        isGrounded = false;
        isWalled = false;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(isWalled);
    }

    public void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
