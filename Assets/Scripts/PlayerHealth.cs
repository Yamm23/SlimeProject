using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 100;   
    public int CurrentHealth;
    public int MaxHearts = 3;
    public int CurrentHearts;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        //CurrentHearts = MaxHearts;
    }
 
    void TakeDamage(int DamageAmount)
    {
        CurrentHealth -= DamageAmount;
        //if (CurrentHealth < 0)
        //{
        //    CurrentHearts -= 1;
        //    ;if (CurrentHearts <= 0)
        //    {
        //        //GameOver
        //    }
        //}
    }
}
