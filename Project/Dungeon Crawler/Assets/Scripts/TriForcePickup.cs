using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriForcePickup : Pickup
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IfPickedUp();
        }
    }

    protected override void IfPickedUp()
    {
        gameManager.coroutine = gameManager.StartCoroutine(gameManager.YouWin());
        base.IfPickedUp();
    }
}
