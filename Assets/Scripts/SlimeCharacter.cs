using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private SlimeMovement slimeMovement;

    void Awake()
    {
        slimeMovement = GetComponent<SlimeMovement>();
    }

    void Update()
    {
        // Call update methods on the individual components if needed
        slimeMovement.HandleMovement();
    }
}

