using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickUp : Pickup
{
    private Sprite icon;
    void Start()
    {
        icon = GetComponent<SpriteRenderer>().sprite;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAnimation temp = collision.GetComponent<PlayerAnimation>();
            temp.swordPickedUp = true;
            temp.SetPlayerState(PlayerState.cheer);
            IfPickedUp();
        }
    }

    protected override void IfPickedUp()
    {
        FindObjectOfType<Equipment>().SetIconA(icon);
        base.IfPickedUp();
    }
}
