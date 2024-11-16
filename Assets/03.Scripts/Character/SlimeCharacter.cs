using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private SlimeMovement slimeMovement;
    private SlimeHealth slimeHealth;
    public CoinManager cm;
    public PauseMenu pauseMenu;

    void Awake()
    {
        slimeMovement = GetComponent<SlimeMovement>();
        slimeHealth = GetComponent<SlimeHealth>();
    }

    void Update()
    {
        // Call update methods on the individual components if needed
        slimeMovement.HandleMovement();
        slimeMovement.HandleDash();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("CoinItem"))
        {
            AudioManager.instance.Play("Coin");
            Destroy(other.gameObject);
            cm.AddCoin();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        { 
            pauseMenu.LevelComplete();
        }
    }
}

