using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    public Rigidbody2D skeletonRigidbody;
    public EnemyPatrol skeletonPatrol;
    public EnemyPlayerDetection skeletonPlayerdetection;
    public EnemyPlayerFollow skeletonPlayerfollow;
    public EnemyAttack skeletonAttack;
    // Start is called before the first frame update

    private void Awake()
    {
        skeletonRigidbody = GetComponent<Rigidbody2D>();
        skeletonPatrol = GetComponent<EnemyPatrol>();
        skeletonPlayerdetection = GetComponent<EnemyPlayerDetection>();
        skeletonPlayerfollow = GetComponent<EnemyPlayerFollow>();
        skeletonAttack = GetComponent<EnemyAttack>();
        
    }


    void Update()
    {
        if (skeletonPlayerdetection.IsPlayerInRange())
        {
            skeletonPlayerfollow.FollowPlayer();
            skeletonAttack.AttackPlayer();
        }
        else
        {
            skeletonPatrol.Patrol();
        }
    }
}
