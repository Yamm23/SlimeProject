using System.Collections;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    private SlimeHealth playerHealth;
    private Transform playerTransform;
    private bool isDamaging = false;

    // Method to initialize the TrapManager with the player reference
    public void Initialize(Transform player)
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

