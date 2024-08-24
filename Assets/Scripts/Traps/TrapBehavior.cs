using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehavior
{
    private SlimeHealth playerHealth;
    private Transform playerTransform;
    private bool isDamaging = false;
    public TrapBehavior(Transform player)
    {
        this.playerTransform = player;
        playerHealth = player.GetComponent<SlimeHealth>();
    }

    public IEnumerator ApplyContinuousDamage(float damage, float interval)
    {
        isDamaging = true;
        while (isDamaging)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            yield return new WaitForSeconds(interval);
        }
    }
    public void StopDamage()
    {
        isDamaging = false;
    }

}
