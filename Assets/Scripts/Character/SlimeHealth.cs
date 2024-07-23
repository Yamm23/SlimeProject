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
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        UpdateHearts();
    }

    void UpdateHearts()
    {
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

    public void TakeDamage(int damageAmount)
    {
        if (!canTakeDamage) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0); // Ensure health does not go below zero
        healthBar.setHealth(currentHealth);
        slimeAnimator.SetTrigger("TakingDamage");

        if (currentHealth <= 0)
        {
            currentHearts--;
            UpdateHearts();

            if (currentHearts <= 0)
            {
                Die();
            }
            else
            {
                currentHealth = maxHealth;
                healthBar.setHealth(currentHealth);
            }
        }
    }

    public void Die()
    {
        UpdateHearts();
        Debug.Log("SlimeDied");
        gameObject.SetActive(false); // Deactivate the slime object
        // Additional game over logic can go here
    }

    void RestartLevel()
    {
        Debug.Log("Restarting Level");
        currentHealth = maxHealth;
        healthBar.setHealth(currentHealth);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

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

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}
