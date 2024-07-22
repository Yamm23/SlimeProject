using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Transform pointA;
    private Transform pointB;
    private Transform currentPoint;
    private Rigidbody2D skeletonRigidbody;
    public float speed;

    void Start()
    {
        currentPoint = pointB; // Ensure currentPoint is initialized
    }

    void Update()
    {
        Patrol();
    }

    public void SetPatrolPoints(Transform pointA, Transform pointB)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        currentPoint = pointB;
    }

    public void SetRigidbody(Rigidbody2D rigidbody)
    {
        skeletonRigidbody = rigidbody;
    }

    public void Patrol()
    {
        if (skeletonRigidbody == null) return; // Ensure the Rigidbody is assigned

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
}
