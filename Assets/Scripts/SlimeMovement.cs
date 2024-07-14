using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float speed = 8.0f;
    public float jumpForce = 25.0f;
    private int jumpCount = 0;
    private bool isGrounded = true;
    public Animator slimeanimator;

    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    public void HandleMovement()
    {
        // Handle horizontal movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * speed, myRigidbody.velocity.y);
        myRigidbody.velocity = movement;
        slimeanimator.SetFloat("HorizontalSpeed", Mathf.Abs(moveHorizontal));

        // Handle jumping
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < 2))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0); // Reset vertical velocity before jumping
            myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
            isGrounded = false; // Player is no longer grounded
            slimeanimator.SetBool("IsJumping", true);
            Debug.Log("Jump button pressed. Current jump count: " + jumpCount);
        }
    }

    // Check if the character is grounded
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Trap"))
        {
            isGrounded = true;
            jumpCount = 0; // Reset jump count when grounded
            slimeanimator.SetBool("IsJumping", false);

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Trap"))
        {
            isGrounded = false;
        }
    }
}
