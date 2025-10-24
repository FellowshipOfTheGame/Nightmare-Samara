using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Health Variables
    [Header("Health")]
    [SerializeField] private int maxHealth = 2;
    private int currentHealth;
    #endregion

    #region Hit Feedback Variables

    [Header("Hit Feedback")]
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float hitDuration = 0.2f; // Duração do feedback
    [SerializeField] private float delay = 2f;
    private Color originalColor; // Cor original do sprite
    #endregion

    #region Detection Variables
    [Header("DetectionSystem")]
    [HideInInspector] public DetectionSystem detection;
    [HideInInspector] public bool facingRight = true;
    public float viewDistance = 4f;
    public float loseTime = 4f;
    public LayerMask detectionMask;
    #endregion

    #region Enemy Components
    [HideInInspector] public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    #endregion

    #region State Machine
    private EnemyStateMachine stateMachine;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public ChaseState chaseState;
    #endregion 

    #region Patrol State
    [Header("Patrol State")]
    public float patrolSpeed = 2f;
    public float patrolDistance = 5f;
    public float waitTime = 1f;
    #endregion

    #region Chase State
    [Header("Chase Settings")]
    public float chaseSpeed = 5f;
    public float acceleration = 80f;
    public float lostPlayerDelay = 0.5f;
    #endregion

    #region Collision Flags
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isWalled;
    #endregion

    #region EnemyType
    private bool isSkelleton =  false;
    private bool isRat = false;
    #endregion

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        detection = new DetectionSystem(this);

        stateMachine = new EnemyStateMachine();
        patrolState = new PatrolState(this, stateMachine);
        chaseState = new ChaseState(this, stateMachine);

        isSkelleton = gameObject.CompareTag("Skeleton");
        isRat = gameObject.CompareTag("Rat");
    }

    void Start()
    {
        currentHealth = maxHealth;
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer não encontrado! O hit feedback não funcionará.");
        }

        stateMachine.Start(patrolState);
    }

    private void Update()
    {
        stateMachine.FrameUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
    }


    #region Health/Hit Feedback
    public void TakeDamage(int amount, ItemType item)
    {
        if (isSkelleton && item == ItemType.WoddenBat)
        {
            currentHealth -= amount;
            //Debug.Log("Vida do inimigo: " + currentHealth);

            if (spriteRenderer != null)
            {
                StartCoroutine(HitFeedback());
            }
        }
        else if (isRat && item == ItemType.PoisonPot) {
            currentHealth -= amount;
            //Debug.Log("Vida do inimigo: " + currentHealth);

            if (spriteRenderer != null)
            {
                StartCoroutine(HitFeedback());
            }
        }        

        if (currentHealth <= 0)
        {
            if (isSkelleton)
            {
                Disassemble();
            }
            if (isRat)
            {
                Die();
            }
        }
    }

    private IEnumerator HitFeedback()
    {
        spriteRenderer.color = hitColor;

        yield return new WaitForSeconds(hitDuration);

        spriteRenderer.color = originalColor;
    }

    private void Disassemble()
    {
        gameObject.SetActive(false);
        Invoke("Reappear", delay);
    }

    void Reappear()
    {
        currentHealth = maxHealth;
        gameObject.SetActive(true);
    }

    private void Die()
    {
        Destroy(gameObject); 
    }
    #endregion
}
