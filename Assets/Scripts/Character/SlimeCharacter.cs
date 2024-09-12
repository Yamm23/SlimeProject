using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private SlimeMovement slimeMovement;
    private SlimeHealth slimeHealth;
    public CoinManager cm;

    void Awake()
    {
        slimeMovement = GetComponent<SlimeMovement>();
        slimeHealth = GetComponent<SlimeHealth>();
    }

    void Update()
    {
        // Call update methods on the individual components if needed
        slimeMovement.HandleMovement();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("CoinItem"))
        {
            Destroy(other.gameObject);
            cm.AddCoin();
        }
    }
}

