using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehavior
{
    private SlimeHealth playerHealth;
    private Transform playerTransform;
    public TrapBehavior(Transform player)
    {
        this.playerTransform = player;
        playerHealth = player.GetComponent<SlimeHealth>();
    }
    public void Start()
    {
        
    }
    public void TakeDamage()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(10);
        }
        else
        {
            Debug.Log("Player Health Component not found");
        }
    }
}
