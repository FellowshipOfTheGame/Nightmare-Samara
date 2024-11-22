using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 5f;

    private float moveInput;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
