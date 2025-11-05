using Unity.Behavior;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public BehaviorGraphAgent behaviorGraph { get; private set; }

    public EnemySceneCheck sceneCheck { get; private set; }


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        behaviorGraph = GetComponent<BehaviorGraphAgent>();
        sceneCheck = GetComponentInChildren<EnemySceneCheck>();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(isWalled);
        //Debug.Log(rb.linearVelocity);
    }

    public void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
