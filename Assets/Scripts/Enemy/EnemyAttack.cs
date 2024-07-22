using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1f;
    public int attackDamage = 10;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void AttackPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            // Implement attack logic here, for example:
            player.GetComponent<SlimeHealth>().TakeDamage(attackDamage);
        }
    }
}
