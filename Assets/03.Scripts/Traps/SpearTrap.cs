using System.Collections;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    public float damageAmount = 20f;
    public float damageInterval = 0.5f;
    public CircleCollider2D tipCollider;
    private TrapManager trapManager;
    private Coroutine damageCoroutine;

    private void Start()
    {
        // Initialize TrapManager
        trapManager = gameObject.AddComponent<TrapManager>(); // Add TrapManager component
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        trapManager.Initialize(playerTransform);

        // Optional: Configure the collider if needed
        tipCollider = GetComponent<CircleCollider2D>();
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
            StopCoroutine(damageCoroutine);  // Stop only the specific coroutine
            damageCoroutine = null;
        }
    }
}
