using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetection();
    }
    public void PlayerDetection()
    {
        float distancetoPlayer = Vector2.Distance(transform.position, playerTransform.position);
        Debug.Log($"Distance to Player :{distancetoPlayer})");
    }
}
