using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    public Rigidbody2D skeletonRigidbody;
    private EnemyPatrol skeletonPatrol;
    private EnemyPlayerDetection skeletonPlayerdetection;
    private EnemyPlayerFollow skeletonPlayerfollow;
    private EnemyAttack skeletonAttack;
    // Start is called before the first frame update

    private void Awake()
    {
        skeletonRigidbody = GetComponent<Rigidbody2D>();
        skeletonPatrol = GetComponent<EnemyPatrol>();
        skeletonPlayerdetection = GetComponent<EnemyPlayerDetection>();
        skeletonPlayerfollow = GetComponent<EnemyPlayerFollow>();
        skeletonAttack = GetComponent<EnemyAttack>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
