using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBoundary : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnPoint;
    private SlimeHealth respawnslimeHealth;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        respawnslimeHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<SlimeHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            this.currentHealth = respawnslimeHealth.currentHealth;
            respawnslimeHealth.TakeDamage(this.currentHealth);
            player.transform.position = respawnPoint.transform.position;
        }
    }
}
