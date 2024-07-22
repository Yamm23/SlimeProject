using System.Collections;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    public Rigidbody2D skeletonRigidbody;
    private Animator skeletonAnim;
    private EnemyBehavior enemyBehavior;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        skeletonAnim = GetComponent<Animator>();

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
            skeletonAnim,
            this // Pass the current instance of SkeletonController
        );
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