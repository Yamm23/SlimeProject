using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public int direction = 1;
    public float speed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        Vector2 target = currentMovementTarget();
        platform.position = Vector2.MoveTowards(platform.position, target, speed * Time.deltaTime);

        // Check if the platform has reached the target
        if (Vector2.Distance(platform.position, target) <= 0.1f)
        {
            direction *= -1; // Reverse the direction
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has landed on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Parent the player to the platform so they move together
            collision.transform.SetParent(platform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player has left the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Unparent the player from the platform
            collision.transform.SetParent(null);
        }
    }

    Vector2 currentMovementTarget()
    {
        return direction == 1 ? endPoint.position : startPoint.position;
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
