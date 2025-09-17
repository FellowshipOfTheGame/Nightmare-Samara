using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    #region Player Systems
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public StaminaSystem staminaSystem;
    #endregion

    #region Health System
    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    private SpriteRenderer rend;
    private Color hitColor = Color.red;
    private Color originalColor;

    [Header("Invincibility Frames")]
    [SerializeField] private float invincibilityDuration = 1f;
    private bool isInvincible = false;
    #endregion

    #region Stamina System
    [Header("Stamina System")]
    public float maxStamina = 10f;
    [HideInInspector] public float currentStamina;
    public float exhaustedWalkSpeed = 3f;
    public float exhaustedJumpForce = 4f;
    public float staminaRegenRate = 2f;
    public float staminaDrainRate = 1f;
    public float minStamina = 18f;
    public bool IsExhausted => currentStamina <= 0;
    #endregion  

    #region Inventory System
    [HideInInspector] public int poisonFlask;
    [HideInInspector] public int woodenBat;
    [HideInInspector] public int itemVida;
    #endregion

    #region Knockback System
    [Header("Knockback")]
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.3f;
    private bool isKnockbackActive = false;
    #endregion

    #region State Machine
    private PlayerStateMachine stateMachine;
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public WalkingState walkingState;
    [HideInInspector] public RunningState runningState;
    [HideInInspector] public ExhaustedState exhaustedState;
    [HideInInspector] public FallingState fallingState;
    [HideInInspector] public JumpingState jumpingState;
    //[HideInInspector] public WallJumpState wallJumpState;
    //[HideInInspector] public WallSlideState wallSlideState;
    [HideInInspector] public bool isGrounded = false;

    #endregion

    #region Walk State
    [Header("Walk State")]
    public float walkSpeed = 3f;
    #endregion

    #region Run State
    [Header("Run State")]
    public float runSpeed = 3f;
    public float acceleration = 100f;
    #endregion

    #region Jump State
    [Header("Jump State")]
    public float jumpForce = 3f;
    public float fallMult = 2.5f;
    public float lowJumpMult = 2f;
    #endregion

    private void Awake()
    {
        staminaSystem = new StaminaSystem(this);
        stateMachine = new PlayerStateMachine();

        idleState = new IdleState(this, stateMachine);
        walkingState = new WalkingState(this, stateMachine);
        runningState = new RunningState(this, stateMachine);
        fallingState = new FallingState(this, stateMachine);
        exhaustedState = new ExhaustedState(this, stateMachine);
        jumpingState = new JumpingState(this, stateMachine);

        poisonFlask = 0;
        woodenBat = 0;
        itemVida = 0;

        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        stateMachine.Start(idleState);
        if (rend != null)
        {
            originalColor = rend.color;
        }
    }

    #region Damage
    public void TakeDamage(int damage)
    {
        if (isInvincible || currentHealth <= 0)
        {
            return;
        }

        currentHealth -= damage;
        Debug.Log("Vida do Personagem: " + currentHealth);

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        if (isInvincible) yield break;
        if (rend == null) yield break;

        isInvincible = true;
        rend.color = (rend.color == originalColor) ? hitColor : originalColor;
        yield return new WaitForSeconds(invincibilityDuration);
        rend.color = originalColor;
        isInvincible = false;
    }

    public IEnumerator ApplyKnockback(Vector2 direction)
    {
        isKnockbackActive = true;

        isInvincible = true;

        rb.velocity = Vector2.zero;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        // Reduz a velocidade gradualmente
        rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y);
        isKnockbackActive = false;

        // Mantém a invencibilidade por um tempo adicional se necessário
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }
    #endregion

    private void Update()
    {
        stateMachine.FrameUpdate();
    }

    private void FixedUpdate()
    {
        if (isKnockbackActive)
        {
            return;
        }
        stateMachine.PhysicsUpdate();
        //Debug.Log("Estamina Atual: " + currentStamina);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Veneno"))
        {
            Destroy(collision.gameObject);
            poisonFlask++;
            Debug.Log("Frascos de veneno: " + poisonFlask);
        }

        if (collision.gameObject.CompareTag("Taco"))
        {
            Destroy(collision.gameObject);
            woodenBat++;
            Debug.Log("Tacos de madeira: " + woodenBat);
        }

        if (collision.gameObject.CompareTag("ItemVida"))
        {   
            Destroy(collision.gameObject);  
            itemVida++;  
            Debug.Log("Item de Vida: " + itemVida);  
        }
    }
}
