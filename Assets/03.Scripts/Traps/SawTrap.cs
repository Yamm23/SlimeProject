using UnityEngine;

public class TrapMovement2D : MonoBehaviour
{
    public float speed = 10f; // Speed of the trap's movement
    public float damageAmount = 40f; // Damage dealt to the player
    public float damageInterval = 0.5f; // Time between damage ticks
    public float soundTriggerDistance = 20.0f; // Distance to trigger sound
    private bool hasPlayedSound = false; // Flag to manage sound state

    public Transform startPoint; // Starting point of the trap's movement
    public Transform endPoint; // End point of the trap's movement
    private int direction = 1; // Direction of movement (1 = towards endPoint, -1 = towards startPoint)
    private TrapManager trapManager;
    private Transform playerTransform;

    void Start()
    {
        // Initialize TrapManager and pass player reference
        trapManager = gameObject.AddComponent<TrapManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        trapManager.Initialize(playerTransform);
    }

    public void SawPlaySound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("Saw");
        }
    }

    public void StopSawSound()
    {
        AudioManager.instance.Stop("Saw");
    }

    void Update()
    {
        MoveTrap();
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer < soundTriggerDistance)
        {
            if (!hasPlayedSound)
            {
                SawPlaySound();
                hasPlayedSound = true;
            }
        }
        else
        {
            if (hasPlayedSound)
            {
                StopSawSound();
                hasPlayedSound = false;
            }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming player has the "SlimeHealth" component
            SlimeHealth playerHealth = other.GetComponent<SlimeHealth>();

            if (playerHealth != null)
            {
                trapManager.StartCoroutine(trapManager.ApplyContinuousDamage(damageAmount, damageInterval));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop applying damage when the player exits the trigger area
            trapManager.StopDamage();
        }
    }

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
