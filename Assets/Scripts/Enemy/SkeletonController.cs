using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    public Rigidbody2D skeletonRigidbody;
    private EnemyPatrol skeletonPatrol;
    private EnemyPlayerDetection skeletonPlayerDetection;
    private EnemyPlayerFollow skeletonPlayerFollow;
    private EnemyAttack skeletonAttack;

    [SerializeField] private Transform playerTransform; // Assign player transform from the Inspector

    private void Awake()
    {
        // Initialize the behavior scripts
        skeletonPatrol = new EnemyPatrol();
        skeletonPlayerDetection = new EnemyPlayerDetection();
        skeletonPlayerFollow = new EnemyPlayerFollow();
        skeletonAttack = new EnemyAttack();

        // Initialize rigidbody
        skeletonRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Point A or Point B not set");
            return;
        }

        // Set patrol points and rigidbody for patrol behavior
        skeletonPatrol.SetPatrolPoints(pointA.transform, pointB.transform);
        skeletonPatrol.SetRigidbody(skeletonRigidbody);
        skeletonPatrol.SetTransform(transform);
        skeletonPatrol.speed = 2.0f; // Set patrol speed as needed

        // Set player for detection, follow, and attack
        skeletonPlayerDetection.SetPlayer(playerTransform);
        skeletonPlayerFollow.SetPlayer(playerTransform);
        skeletonAttack.SetPlayer(playerTransform);
    }

    private void Update()
    {
        if (skeletonPlayerDetection.IsPlayerInRange(skeletonRigidbody.transform.position))
        {
            skeletonPlayerFollow.FollowPlayer(skeletonRigidbody.transform);
            skeletonAttack.AttackPlayer(skeletonRigidbody.transform);
        }
        else
        {
            skeletonPatrol.Patrol();
        }
    }
}
