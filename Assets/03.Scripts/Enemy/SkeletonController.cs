using System.Collections;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] GameObject slimeCharacter;
    public Rigidbody2D skeletonRigidbody;
    public float speed = 3f; // Default speed
    public float detectionRange = 5f; // Default detection range
    public float attackRange = 2f; // Default attack range
    private Animator skeletonAnimator;
    private EnemyBehavior enemyBehavior;

    private void Start()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        skeletonAnimator = GetComponent<Animator>();

        if (pointA == null || pointB == null || skeletonRigidbody == null || playerTransform == null)
        {
            Debug.LogError("Required components are not assigned.");
            return;
        }

        // Add EnemyBehavior as a component to the SkeletonController GameObject
        enemyBehavior = gameObject.AddComponent<EnemyBehavior>();

        // Initialize the EnemyBehavior script
        enemyBehavior.Setup(
            pointA.transform,
            pointB.transform,
            playerTransform,
            skeletonRigidbody,
            transform,
            skeletonAnimator,
            this,
            speed,
            detectionRange,
            attackRange
        );
    }

    private void Update()
    {
        if (enemyBehavior != null)
        {
            enemyBehavior.UpdateBehavior();
        }
    }

    public void StartAttackCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
