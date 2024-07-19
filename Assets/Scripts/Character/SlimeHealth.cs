using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public Animator slimeanimator;

    void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
        maxHearts = currentHearts;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i< maxHealth; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
    // Method to apply damage to the slime
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health does not go below zero
        healthBar.setHealth(currentHealth);
        slimeanimator.SetTrigger("TakingDamage");

        if (currentHealth <= 0)
        {
            currentHearts--;
            if(currentHearts < 0)
            {
                Die();
            }
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
