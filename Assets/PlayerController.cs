using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 5f;

    bool hasWeapon = false;
    bool hasRange = false;

    float moveInput;
    bool isGrounded;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //Debug.Log(isGrounded);
        //Movimento Vertical
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
        }

        if (hasWeapon)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && hasRange)
            {
                Debug.Log("BATEU");
            }
        }
    }

    void FixedUpdate()
    {
        //Movimento Horizontal
        moveInput = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("D");
            moveInput = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("A");
            moveInput = -1f;
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            hasWeapon = true;
            Destroy(collision.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasRange = false;
        }
    }
}