using UnityEngine;

public class TrapMovement2D : MonoBehaviour
{
    public float speed = 2f;                  // Speed of the trap's movement
    public float damageAmount = 20f;          // Damage dealt to the player
    public float damageInterval = 0.5f;       // Time between damage ticks
    private Coroutine damageCoroutine;

    public Transform startPoint;              // Starting point of the trap's movement
    public Transform endPoint;                // End point of the trap's movement
    private int direction = 1;                // Direction of movement (1 = towards endPoint, -1 = towards startPoint)

    private TrapManager trapManager;

    void Start()
    {
        // Initialize TrapManager and pass player reference
        trapManager = gameObject.AddComponent<TrapManager>(); // Add TrapManager component
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        trapManager.Initialize(playerTransform);
    }

    void Update()
    {
        MoveTrap();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(trapManager.ApplyContinuousDamage(damageAmount, damageInterval));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && damageCoroutine != null)
        {
            trapManager.StopDamage();
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    void MoveTrap()
    {
        // Determine the target position based on the current direction
        Vector2 targetPosition = direction == 1 ? endPoint.position : startPoint.position;

        // Move the trap towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the trap has reached the target position
        if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
        {
            // Reverse the direction
            direction *= -1;
        }
    }

    // Draw Gizmos to visualize the start and end points
    private void OnDrawGizmos()
    {
        if (startPoint != null && endPoint != null)
        {
            // Draw a line between startPoint and endPoint
            Gizmos.color = Color.green;
            Gizmos.DrawLine(startPoint.position, endPoint.position);

            // Draw a sphere at the startPoint
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(startPoint.position, 0.1f);

            // Draw a sphere at the endPoint
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(endPoint.position, 0.1f);
        }
    }
}
