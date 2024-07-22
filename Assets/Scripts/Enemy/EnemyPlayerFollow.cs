using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerFollow : MonoBehaviour
{
    private Transform player;
    public float followSpeed = 2.0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FollowPlayer()
    {
        if (player == null) return;
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
    }
}
