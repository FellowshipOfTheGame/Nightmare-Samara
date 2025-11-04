using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public InputSystem inputSystem { get; private set; }
    public StateMachine stateMachine { get; private set; }

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isWalled;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputSystem = GetComponent<InputSystem>();
        stateMachine = GetComponent<StateMachine>();
    }

    public void Start()
    {
        isGrounded = false;
        isWalled = false;
    }


    public void Flip()
    {
        float input = inputSystem.MoveInput.x;
        if (input > 0)
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        else if (input < 0)
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }
}
