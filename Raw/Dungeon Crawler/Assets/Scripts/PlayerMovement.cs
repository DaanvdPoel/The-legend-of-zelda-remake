using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 input;
    [SerializeField] private float movementSpeed;
    Rigidbody2D rb;

    private PlayerAnimation playerAnimation;

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (playerAnimation.playerState != PlayerState.dead)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
        }

        if(input.y != 0)
        {
            input.x = 0;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = (input * movementSpeed);

        if(playerAnimation.playerState == PlayerState.dead)
        {
            rb.velocity = new Vector3(0,0,0);
        }
    }
}