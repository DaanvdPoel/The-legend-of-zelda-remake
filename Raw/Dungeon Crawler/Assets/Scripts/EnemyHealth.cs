using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private float maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    protected override void Kill()
    {
        base.Kill();
        Pickup pickup = GetComponent<Pickup>();
        if(pickup != null)
        {
            pickup.SpawnPickup(transform.position);
        }
        //play audio
        Destroy(gameObject);
    }

    protected override void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
