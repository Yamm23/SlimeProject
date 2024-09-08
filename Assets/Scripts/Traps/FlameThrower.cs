using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public GameObject flame;
    private Transform playerTransform;
    private Animator fireboxAnim;
    public float detectionRange = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        fireboxAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDetection();
    }
    public void PlayerDetection()
    {
        float distancetoPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if(distancetoPlayer <= detectionRange)
        {
            fireboxAnim.SetBool("inRange", true);
        }
        else
        {
            fireboxAnim.SetBool("inRange", false);
        }
    }
    public void ActivateFlame()
    {
        flame.SetActive(true);
    }
    public void DeactivateFlame()
    {
        flame.SetActive(false);
    }
}
