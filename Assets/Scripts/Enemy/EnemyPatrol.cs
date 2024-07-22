using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Transform pointA;
    private Transform pointB;
    private Rigidbody2D skeletonRigidbody;
    private Transform currentPoint;
    public float speed;

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

    void Update()
    {
        if (currentPoint == null || skeletonRigidbody == null) return;

        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == pointB)
        {
            skeletonRigidbody.velocity = new Vector2(speed, skeletonRigidbody.velocity.y);
        }
        else
        {
            skeletonRigidbody.velocity = new Vector2(-speed, skeletonRigidbody.velocity.y);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPoint = (currentPoint == pointB) ? pointA : pointB;
        }
    }

    public void Patrol()
    {
        // Your existing patrol logic here
    }
}
