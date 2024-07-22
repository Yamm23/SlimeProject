using UnityEngine;

public class EnemyPlayerFollow
{
    private Transform player;
    public float followSpeed = 2.0f;

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void FollowPlayer(Transform enemyTransform)
    {
        if (player == null) return;
        Vector2 direction = (player.position - enemyTransform.position).normalized;
        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, player.position, followSpeed * Time.deltaTime);
    }
}
