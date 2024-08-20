using Unity.VisualScripting;
using UnityEngine;

public class TrapMovement2D : MonoBehaviour
{
    public float speed = 2f;              // Speed of the trap's movement
    public float moveDistance = 5f;       // Distance the trap will move up and down
    private Vector2 startPosition;        // Starting position of the trap
    private bool movingUp = true;         // Direction flag
    private TrapBehavior trapBehavior;
    public GameObject slimeCharacter;

    void Start()
    {
        trapBehavior = new TrapBehavior();
        slimeCharacter = GameObject.FindWithTag("Player");
        startPosition = transform.position;  // Record the starting position
    }

    void Update()
    {
        MoveTrap();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            trapBehavior.TakeDamage();
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
