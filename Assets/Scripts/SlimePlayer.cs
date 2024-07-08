using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePlayer : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float speed = 5.0f;
    public float jumpForce = 25.0f;
    private int jumpCount;
    private bool isGrounded;

    void Update()
    {
        // Handle horizontal movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * speed, myRigidbody.velocity.y);
        myRigidbody.velocity = movement;

        // Handle jumping
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            Debug.Log("Jump button pressed. Current jump count: " + jumpCount);
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0); // Reset vertical velocity before jumping
            myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
            Debug.Log("Jump executed. New jump count: " + jumpCount);
        }
    }

    // Check if the character is grounded
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0; // Reset jump count when grounded
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
