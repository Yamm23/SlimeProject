using System.Collections;
using UnityEngine;

public class EnemyBehavior
{
    private Transform pointA;
    private Transform pointB;
    private Transform player;
    private Rigidbody2D enemyRigidbody;
    private Transform enemyTransform;
    private Animator enemyAnim;
    private Transform currentPoint;
    public float speed=5f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    private bool isPlayerInRange = false;
    private bool isAttacking = false;
    private bool facingRight = false;

    private SkeletonController controller;

    // Constructor to initialize all required fields
    public EnemyBehavior(Transform pointA, Transform pointB, Transform player, Rigidbody2D rb, Transform enemyTransform, Animator anim, SkeletonController controller)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.player = player;
        this.enemyRigidbody = rb;
        this.enemyTransform = enemyTransform;
        this.enemyAnim = anim;
        this.controller = controller;
        currentPoint = pointB;
    }

    public void Update()
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

        if (isPlayerInRange && Vector2.Distance(enemyTransform.position, player.position) <= attackRange)
        {
            if (!isAttacking) controller.StartAttackCoroutine(AttackPlayer());
        }
    }

    private void Patrol()
    {
        Vector2 direction = currentPoint.position - enemyTransform.position;

        if (currentPoint == pointB)
        {
            enemyRigidbody.velocity = new Vector2(speed, enemyRigidbody.velocity.y);
        }
        else
        {
            enemyRigidbody.velocity = new Vector2(-speed, enemyRigidbody.velocity.y);
        }

        if (enemyRigidbody.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (enemyRigidbody.velocity.x < 0 && facingRight)
        {
            Flip();
        }

        if (Vector2.Distance(enemyTransform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = (currentPoint == pointB) ? pointA : pointB;
        }
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(enemyTransform.position, player.position);
        isPlayerInRange = distanceToPlayer <= detectionRange;
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.position - enemyTransform.position).normalized;
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
        isAttacking = true;
        enemyAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f); // Adjust as necessary for your attack animation
        isAttacking = false;
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
        if (Mathf.Abs(enemyRigidbody.velocity.x) > 0.1f)
        {
            enemyAnim.SetBool("isWalking", true);
        }
        else
        {
            enemyAnim.SetBool("isWalking", false);
        }
    }
}
