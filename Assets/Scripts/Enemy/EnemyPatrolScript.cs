using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolScript : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public Rigidbody2D skeletonRigidbody;
    private Animator skeletonAnim;
    private Transform currentPoint;
    public float speed;
    private bool facingRight = false; // Flag to track the direction the skeleton is facing

    // Start is called before the first frame update
    void Start()
    {
        skeletonRigidbody = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        skeletonAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();

        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == pointB.transform)
        {
            skeletonRigidbody.velocity = new Vector2(speed, skeletonRigidbody.velocity.y);
        }
        else
        {
            skeletonRigidbody.velocity = new Vector2(-speed, skeletonRigidbody.velocity.y);
        }

        // Check if the skeleton needs to flip direction
        if (skeletonRigidbody.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (skeletonRigidbody.velocity.x < 0 && facingRight)
        {
            Flip();
        }

        // Check if the skeleton has reached its patrol point
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }

    // Method to flip the skeleton's sprite
    void Flip()
    {
        facingRight = !facingRight; // Toggle the facingRight flag

        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Flip the x scale of the skeleton
        transform.localScale = theScale;
    }
    private void HandleAnimation()
    {
        if (Mathf.Abs(skeletonRigidbody.velocity.x) > 0.1f)
        {
            skeletonAnim.SetBool("isWalking", true);
        }
        else
        {
            skeletonAnim.SetBool("isWalking", false);
        }
    }
}
