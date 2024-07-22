using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform player;
    public float attackRange = 1.5f;
    public int attackDamage = 10;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void AttackPlayer()
    {
        if (player == null) return;
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("Attacking the player!");
            // Implement attack logic here, e.g., reducing player health
        }
    }
}
