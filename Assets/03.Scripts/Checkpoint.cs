using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private RespawnBoundary respawn;
    private BoxCollider2D checkpointCollider;
    public Animator checkpointAnimation;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        checkpointCollider = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<RespawnBoundary>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            checkpointAnimation.SetTrigger("isActive");
            respawn.respawnPoint = this.gameObject;
            checkpointCollider.enabled = false;
           
        }
    }
}
