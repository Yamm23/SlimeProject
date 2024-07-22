using System.Collections;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    public Rigidbody2D skeletonRigidbody;
    public float speed = 3f; // Default speed
    public float detectionRange = 5f; // Default detection range
    public float attackRange = 2f; // Default attack range
    private Animator skeletonAnimator;
    private EnemyBehavior enemyBehavior;
    private Transform player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        skeletonAnimator = GetComponent<Animator>();

        if (pointA == null || pointB == null || skeletonRigidbody == null || player == null)
        {
            Debug.LogError("Required components are not assigned.");
            return;
        }

        // Create an instance of EnemyBehavior with the required parameters
        enemyBehavior = new EnemyBehavior(
            pointA.transform,
            pointB.transform,
            player,
            skeletonRigidbody,
            transform,
            skeletonAnimator,
            this // Pass the current instance of SkeletonController
        );

        // Set parameters for EnemyBehavior
        enemyBehavior.SetParameters(speed, detectionRange, attackRange);
    }

    private void Update()
    {
        if (enemyBehavior != null)
        {
            enemyBehavior.Update();
        }
    }

    public void StartAttackCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
