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

    private bool losing = false;
    private bool lostPlayer = false;
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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        detection = new DetectionSystem(this);

        stateMachine = new EnemyStateMachine();
        patrolState = new PatrolState(this, stateMachine);
        chaseState = new ChaseState(this, stateMachine);
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
    public void Damage()
    {
        // Reduz a vida do inimigo se estiver com o item certo em mãos
        currentHealth -= 1;
        Debug.Log("Vida do inimigo: " + currentHealth);

        // Inicia o feedback visual
        if (spriteRenderer != null)
        {
            StartCoroutine(HitFeedback());
        }

        if (currentHealth <= 0)
        {
            if (gameObject.CompareTag("Skeleton"))
            {
                Disassemble();
            }
            if (gameObject.CompareTag("Rat"))
            {
                Die();
            }
        }
    }

    private IEnumerator HitFeedback()
    {
        // Muda a cor para a cor de hit
        spriteRenderer.color = hitColor;

        // Aguarda o tempo de feedback
        yield return new WaitForSeconds(hitDuration);

        // Restaura a cor original
        spriteRenderer.color = originalColor;
    }

    private void Disassemble()
    {
        // Faz o GameObject desaparecer
        gameObject.SetActive(false);

        // Reaparece após o tempo especificado
        Invoke("Reappear", delay);
    }

    void Reappear()
    {
        // Faz o esqueleto aparecer novamente
        currentHealth = maxHealth;
        gameObject.SetActive(true);
    }

    private void Die()
    {
        Destroy(gameObject); // Remove o inimigo da cena
    }
    #endregion
}
