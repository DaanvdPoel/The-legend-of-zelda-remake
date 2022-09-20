using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal connectedPortal;

    private GameManager gameManager;
    private Transform playerTransform;
    private bool steppedOut = true;
    private float triggerTime = 0.25f;
    private float stayTimer = 0;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        connectedPortal.steppedOut = steppedOut;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && connectedPortal.steppedOut == true && steppedOut == true)
        {
            gameManager.TeleportPlayerToPortal(connectedPortal);
            steppedOut = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            steppedOut = true;
        }
    }

    public void ReceiveObject(Transform objectTransform)
    {
        objectTransform.position = new Vector3(transform.position.x, transform.position.y + -2f,objectTransform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, connectedPortal.transform.position);
    }
}
