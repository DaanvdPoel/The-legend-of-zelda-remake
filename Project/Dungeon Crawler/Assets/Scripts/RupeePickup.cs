using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeePickup : Pickup
{
    private Equipment equipment;
    private void Awake()
    {
        equipment = FindObjectOfType<Equipment>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            equipment.rupeeCount = equipment.rupeeCount + 1;
            IfPickedUp();
        }
    }

    protected override void IfPickedUp()
    {
        base.IfPickedUp();
    }
}
