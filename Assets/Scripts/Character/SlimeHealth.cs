using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlimeHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int maxHearts = 3;
    public int currentHearts = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public HealthBar healthBar;
    private int damageCount = 0;
    private bool canTakeDamage = true;
    public float damageCooldown = 1.0f;
    public Animator slimeAnimator;

    void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        UpdateHearts();
    }

    void Update()
    {
        UpdateHearts();
    }

    void UpdateHearts()
    {
        // Update the heart UI based on currentHearts
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHearts)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    // Method to apply damage to the slime
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health does not go below zero
        healthBar.setHealth(currentHealth);
        slimeAnimator.SetTrigger("TakingDamage");

        if (currentHealth <= 0)
        {
            currentHearts--;
            if (currentHearts <= 0)
            {
                Die();
            }
            else
            {
                RestartLevel();
            }
        }
    }

    // Method to handle the slime's death
    public void Die()
    {
        Debug.Log("SlimeDied");
        gameObject.SetActive(false); // Deactivate the slime object
        // You may want to add additional logic here, such as displaying a game over screen
    }

    // Method to restart the current level
    void RestartLevel()
    {
        Debug.Log("Restarting Level");
        currentHealth = maxHealth;
        healthBar.setHealth(currentHealth);
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
