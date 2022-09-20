using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float currentHealth;
    
    public virtual void ChangeHealth(float amount)
    {
        currentHealth = currentHealth + amount;
        CheckHealth();
    }

    protected virtual void CheckHealth()
    {
    }

    protected virtual void Kill()
    {

    }
}
