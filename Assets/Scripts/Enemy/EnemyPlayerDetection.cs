using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetection : MonoBehaviour
{
    public float detectionRadius = 5f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= detectionRadius;
    }
}
