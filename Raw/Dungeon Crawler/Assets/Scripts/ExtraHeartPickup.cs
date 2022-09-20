using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHeartPickup : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.MoreMaxHealth();
    }
}
