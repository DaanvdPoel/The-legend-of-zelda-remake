using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeOpener : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.OpenBrige();
        }
    }
}
