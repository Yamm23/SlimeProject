using Unity.VisualScripting;
using UnityEngine;

public class TrapMovement2D : MonoBehaviour
{
    public float speed = 2f;              // Speed of the trap's movement
    public float moveDistance = 5f;       // Distance the trap will move up and down
    public float damageAmount = 20f;
    public float damageInterval = 0.5f;
    private Vector2 startPosition;        // Starting position of the trap
    private bool movingUp = true;         // Direction flag
    private TrapBehavior trapBehavior;
    private Transform playerTransforms;
    private Coroutine damageCoroutine;

    void Start()
    {
        playerTransforms = GameObject.FindGameObjectWithTag("Player").transform;
        trapBehavior = new TrapBehavior(playerTransforms);
        startPosition = transform.position;  // Record the starting position

    }

    void Update()
    {
        MoveTrap();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(trapBehavior.ApplyContinuousDamage(damageAmount, damageInterval));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && damageCoroutine !=null)
        {
            trapBehavior.StopDamage();
            StopAllCoroutines();
            damageCoroutine = null;
        }
    }

    void MoveTrap()
    {
        // Calculate the target position based on direction
        float targetY = movingUp ? startPosition.y + moveDistance : startPosition.y - moveDistance;
        Vector2 targetPosition = new Vector2(transform.position.x, targetY);

        // Move the trap towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the trap reached the target position
        if (Mathf.Abs(transform.position.y - targetY) < 0.1f)
        {
            // Reverse the direction
            movingUp = !movingUp;
        }
    }
}
