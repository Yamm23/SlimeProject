using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerFollow : MonoBehaviour
{
    private Transform player;
    public float followSpeed = 2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void FollowPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * followSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
