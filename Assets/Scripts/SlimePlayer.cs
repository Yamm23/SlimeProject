using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePlayer : MonoBehaviour
{
    public Rigidbody2D myrigidBody;
    public float speed = 5.0f;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        myrigidBody.MovePosition(myrigidBody.position+movement*speed*Time.fixedDeltaTime);
    }
}
