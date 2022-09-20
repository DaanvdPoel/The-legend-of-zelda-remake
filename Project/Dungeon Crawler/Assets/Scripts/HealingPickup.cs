using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPickup : Pickup
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth temp = collision.GetComponent<PlayerHealth>();
            temp.ChangeHealth(+1);
            IfPickedUp();
        }
    }

    protected override void IfPickedUp()
    {
        base.IfPickedUp();
    }
}
