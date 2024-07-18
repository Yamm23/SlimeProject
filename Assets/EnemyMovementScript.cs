using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{
    public Rigidbody enemyRigidBody2D;
    public float enemySpeed = 8.0f;
    private bool isFacingRight = true;

    public void Awake()
    {
        enemyRigidBody2D = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
