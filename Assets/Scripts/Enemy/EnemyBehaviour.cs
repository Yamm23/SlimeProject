using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Transform pointA;
    private Transform pointB;
    private Transform playerTransform;
    private Rigidbody2D enemyRigidbody;
    private Transform enemyTransform;
    private Animator enemyAnim;
    private Transform currentPoint;
    private float speed;
    private float detectionRange;
    private float attackRange;
    private bool isPlayerInRange = false;
    private bool isAttacking = false;
    private bool facingRight = false; // Set to true by default
    private SkeletonController controller;
    private SlimeHealth playerHealth;

    // Setup method for initializing all required fields
    public void Setup(Transform pointA, Transform pointB, Transform player, Rigidbody2D rb, Transform enemyTransform, Animator anim, SkeletonController controller, float speed, float detectionRange, float attackRange)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.playerTransform = player;
        this.enemyRigidbody = rb;
        this.enemyTransform = enemyTransform;
        this.enemyAnim = anim;
        this.controller = controller;
        this.playerHealth = player.GetComponent<SlimeHealth>();
        this.speed = speed;
        this.detectionRange = detectionRange;
        this.attackRange = attackRange;
        currentPoint = pointB;
    }

    public void UpdateBehavior()
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

        if (isPlayerInRange && Vector2.Distance(enemyTransform.position, playerTransform.position) <= attackRange)
        {
            if (!isAttacking)
            {
                controller.StartAttackCoroutine(AttackPlayer());
            }
        }
    }

    private void Patrol()
    {
        // Get the current position and target position
        Vector2 currentPosition = new Vector2(enemyTransform.position.x, 0);
        Vector2 targetPosition = new Vector2(currentPoint.position.x, 0);

        // Calculate the direction and distance
        Vector2 direction = targetPosition - currentPosition;
        float distanceToPoint = Vector2.Distance(currentPosition, targetPosition);

        // Set the velocity based on the direction
        enemyRigidbody.velocity = new Vector2((currentPoint == pointB ? speed : -speed), enemyRigidbody.velocity.y);

        // Flip sprite based on direction
        if (enemyRigidbody.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (enemyRigidbody.velocity.x < 0 && facingRight)
        {
            Flip();
        }

        // Change the patrol point if close enough to the current point
        if (distanceToPoint < 0.5f)
        {
            currentPoint = (currentPoint == pointB) ? pointA : pointB;
        }
    }


    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(enemyTransform.position, playerTransform.position);
        isPlayerInRange = distanceToPlayer <= detectionRange;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (playerTransform.position - enemyTransform.position).normalized;
        enemyRigidbody.velocity = new Vector2(direction.x * speed, enemyRigidbody.velocity.y);

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
        Debug.Log("Attacking the player!");
        isAttacking = true;
        enemyAnim.SetBool("isAttacking", true);
        if (Vector2.Distance(enemyTransform.position, playerTransform.position) <= attackRange)
        {
            playerHealth.TakeDamage(20); // Adjust damage value as needed
        }
        yield return new WaitForSeconds(1f); // Adjust as necessary for your attack animation
        isAttacking = false;
        enemyAnim.SetBool("isAttacking", false);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = enemyTransform.localScale;
        theScale.x *= -1;
        enemyTransform.localScale = theScale;
    }

    private void HandleAnimation()
    {
        enemyAnim.SetBool("isWalking", Mathf.Abs(enemyRigidbody.velocity.x) > 0.1f);
    }
}
