using UnityEngine;

public class EnemyAttack
{
    private Transform player;
    public float attackRange = 1.5f;
    public int attackDamage = 10;

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void AttackPlayer(Transform enemyTransform)
    {
        if (player == null) return;
        if (Vector2.Distance(enemyTransform.position, player.position) <= attackRange)
        {
            Debug.Log("Attacking the player!");
            // Implement attack logic here, e.g., reducing player health
        }
    }
}
