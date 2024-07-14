using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHealth : MonoBehaviour
{
    public int MaxHealth = 100;   
    public int CurrentHealth;
    //public int MaxHearts = 3;
    //public int CurrentHearts;
    public HealthBar healthBar;
    public void Start()
    {
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        //CurrentHearts = MaxHearts;
    }

    public void TakeDamage(int DamageAmount)
    {
        CurrentHealth -= DamageAmount;
        healthBar.setHealth(CurrentHealth);
        if (CurrentHealth < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("SlimeDied");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Trap")) {
            TakeDamage(10);
            Debug.Log("DamageTakenFromTrap");
        }
    }
}
