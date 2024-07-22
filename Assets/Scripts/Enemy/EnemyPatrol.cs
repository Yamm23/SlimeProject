using UnityEngine;

public class EnemyPatrol
{
    private Transform pointA;
    private Transform pointB;
    private Rigidbody2D skeletonRigidbody;
    private Transform enemyTransform;
    private Transform currentPoint;
    public float speed;
    private bool facingRight = false;

    public void SetPatrolPoints(Transform pointA, Transform pointB)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        currentPoint = pointB;
    }

    public void SetRigidbody(Rigidbody2D rb)
    {
        skeletonRigidbody = rb;
    }

    public void SetTransform(Transform transform)
    {
        enemyTransform = transform;
    }

    public void Patrol()
    {
        if (currentPoint == null || skeletonRigidbody == null || enemyTransform == null) return;

        Vector2 direction = currentPoint.position - skeletonRigidbody.transform.position;

        if (currentPoint == pointB)
        {
            skeletonRigidbody.velocity = new Vector2(speed, skeletonRigidbody.velocity.y);
        }
        else
        {
            skeletonRigidbody.velocity = new Vector2(-speed, skeletonRigidbody.velocity.y);
        }

        if (Vector2.Distance(skeletonRigidbody.transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = (currentPoint == pointB) ? pointA : pointB;
        }

        if (skeletonRigidbody.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (skeletonRigidbody.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = enemyTransform.localScale;
        theScale.x *= -1;
        enemyTransform.localScale = theScale;
    }
}
