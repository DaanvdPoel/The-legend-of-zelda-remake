using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : Pickup
{
    private Equipment equipment;
    private void Awake()
    {
        equipment = FindObjectOfType<Equipment>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            IfPickedUp();
        }
    }

    protected override void IfPickedUp()
    {
        equipment.keyCount = equipment.keyCount + 1;
        base.IfPickedUp();
    }
}