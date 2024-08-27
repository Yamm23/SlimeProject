using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    public float damageAmount = 20f;
    public float damageInterval = 0.5f;
    public CircleCollider2D tipCollider;
    private Transform playerTransforms;
    private TrapBehavior trapBehavior;
    private Coroutine damageCoroutine;

    private void Start()
    {
        playerTransforms = GameObject.FindGameObjectWithTag("Player").transform;
        trapBehavior = new TrapBehavior(playerTransforms);
        tipCollider = GetComponent<CircleCollider2D>();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(trapBehavior.ApplyContinuousDamage(damageAmount, damageInterval));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && damageCoroutine != null)
        {
            trapBehavior.StopDamage();
            StopAllCoroutines();
            damageCoroutine = null;
        }
    }
}
