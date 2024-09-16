using System.Collections;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    public float damageAmount = 20f;
    public float damageInterval = 0.5f;
    public float soundTriggerDistance = 20.0f; // Fixed spelling
    public CircleCollider2D tipCollider;
    private TrapManager trapManager;
    private Coroutine damageCoroutine;
    private Transform playerTransform;

    private void Start()
    {
        // Initialize TrapManager
        trapManager = gameObject.AddComponent<TrapManager>();

        // Find the player and initialize TrapManager
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object with tag 'Player' not found.");
            return;
        }
        playerTransform = player.transform; // Correctly assign the field

        trapManager.Initialize(playerTransform);

        // Optional: Configure the collider if needed
        tipCollider = GetComponent<CircleCollider2D>();
    }

    public void PlaySpearSound()
    {
        if (AudioManager.instance != null)
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

            // Only play the sound if the player is within the specified range
            if (distanceToPlayer < soundTriggerDistance)
            {
                AudioManager.instance.Play("Spear");
            }
        }
        else
        {
            Debug.LogError("AudioManager instance is not set.");
        }
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
