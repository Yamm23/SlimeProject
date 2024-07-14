using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private SlimeMovement slimeMovement;
    private SlimeHealth slimeHealth;

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
}

