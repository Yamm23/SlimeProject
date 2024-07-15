using System.Collections;
using UnityEngine;

public class SlimeHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the slime
    private int currentHealth;  // Current health of the slime
    public HealthBar healthBar; // Reference to the health bar UI element

    private int damageCount = 0; // Counter for the number of times damage is taken
    private bool canTakeDamage = true; // Flag to control damage cooldown
    public float damageCooldown = 1.0f; // Cooldown time in seconds

    void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Method to apply damage to the slime
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health does not go below zero
        healthBar.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle the slime's death
    public void Die()
    {
        Debug.Log("SlimeDied");
        gameObject.SetActive(false); // Deactivate the slime object
    }

    // Method called when a collision with another collider occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap") && canTakeDamage)
        {
            TakeDamage(10);
            damageCount++;
            Debug.Log($"Damage taken from trap. Number of times damaged: {damageCount}");
            StartCoroutine(DamageCooldown());
        }
    }

    // Coroutine to handle the damage cooldown
    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}
