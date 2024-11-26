using System.Collections;
using UnityEngine;

public class EnemyPatrolScript : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public Rigidbody2D skeletonRigidbody;
    private Animator skeletonAnim;
    private Transform currentPoint;
    public float speed;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    private Transform player;
    private bool isPlayerInRange = false;
    private bool isAttacking = false;
    private bool facingRight = false; // Flag to track the direction the skeleton is facing

    // Start is called before the first frame update
    void Start()
    {
        skeletonRigidbody = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        skeletonAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimation();

        if (!isPlayerInRange && !isAttacking)
        {
            Patrol();
        }

        DetectPlayer();

        if (isPlayerInRange && !isAttacking)
        {
            FollowPlayer();
        }

        if (isPlayerInRange && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    void Patrol()
    {
        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == pointB.transform)
        {
            skeletonRigidbody.linearVelocity = new Vector2(speed, skeletonRigidbody.linearVelocity.y);
        }
        else
        {
            skeletonRigidbody.linearVelocity = new Vector2(-speed, skeletonRigidbody.linearVelocity.y);
        }

        // Check if the skeleton needs to flip direction
        if (skeletonRigidbody.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (skeletonRigidbody.linearVelocity.x < 0 && facingRight)
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

    void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        skeletonRigidbody.linearVelocity = new Vector2(direction.x * speed, skeletonRigidbody.linearVelocity.y);

        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        skeletonAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f); // Attack animation duration
        isAttacking = false;
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
        if (Mathf.Abs(skeletonRigidbody.linearVelocity.x) > 0.1f)
        {
            skeletonAnim.SetBool("isWalking", true);
        }
        else
        {
            skeletonAnim.SetBool("isWalking", false);
        }
    }
}
