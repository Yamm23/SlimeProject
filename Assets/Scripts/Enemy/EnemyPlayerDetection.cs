using UnityEngine;

public class EnemyPlayerDetection
{
    public float detectionRange = 5.0f;
    private Transform player;

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    public bool IsPlayerInRange(Vector2 enemyPosition)
    {
        if (player == null) return false;
        return Vector2.Distance(enemyPosition, player.position) <= detectionRange;
    }
}
