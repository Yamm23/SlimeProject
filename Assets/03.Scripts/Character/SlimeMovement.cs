using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public float speed = 8.0f;
    public float jumpForce = 25.0f;
    private int jumpCount = 0;
    public float dashSpeed = 100.0f;
    public float dashDuration = 1.0f;
    [SerializeField] private float dashCooldown = 1.0f;

    public Animator slimeanimator;
    public Rigidbody2D myRigidbody;

    private bool isGrounded = true;
    private bool facingRight = true;
    private bool isDashing = false;

    private float dashCooldownTime;
    


    public void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void HandleMovement()
    {
        if (isDashing) return;
        // Handle horizontal movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * speed, myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity = movement;
        slimeanimator.SetFloat("HorizontalSpeed", Mathf.Abs(moveHorizontal));

        // Flip the character's facing direction
        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < 2))
        {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, 0); // Reset vertical velocity before jumping
            //AudioManager.instance.Play("Jump");
            myRigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
            isGrounded = false; // Player is no longer grounded
            slimeanimator.SetBool("IsJumping", true);
            Debug.Log("Jump button pressed. Current jump count: " + jumpCount);
        }
        
    }
    public void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > dashCooldownTime)
        {
            StartCoroutine(Dash());
        }
    }
    // Check if the character is grounded
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Trap"))
        {
            isGrounded = true;
            jumpCount = 0; // Reset jump count when grounded
            slimeanimator.SetBool("IsJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Trap"))
        {
            isGrounded = false;
        }
    }

    // Flip the character's facing direction
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        Debug.Log("Dashing Accessed!!!");
        float originalSpeed = speed;
        Debug.Log("Dash direction: " + transform.localScale.x);
        myRigidbody.linearVelocity = new Vector2(transform.localScale.x * dashSpeed, myRigidbody.linearVelocity.y);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        dashCooldownTime = Time.time + dashCooldown;
        myRigidbody.linearVelocity = Vector2.zero;
    }
}
