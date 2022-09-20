using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LookDirection
{
    up = 0,
    left,
    right,
    down
}

public enum PlayerState
{
    idle = 0,
    walking,
    attacking,
    dead,
    cheer
}
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private LookDirection lookDirection;
    public PlayerState playerState;

    [Header("Input")]

    private float xAxis;
    private float yAxis;

    [SerializeField] private Color[] flashColors;
    private SpriteRenderer playerRenderer;

    public bool swordPickedUp = false;

    private GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerState = PlayerState.idle;
        lookDirection = LookDirection.down;
        playerRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        CheckMovement();
        UseItemSlot();
    }

    private void CheckMovement()
    {
        if (playerState != PlayerState.dead)
        {
            xAxis = Input.GetAxis("Horizontal");
            yAxis = Input.GetAxis("Vertical");

            if (xAxis == 0 && yAxis == 0)
            {
                playerState = PlayerState.idle;
            }
            else
            {
                playerState = PlayerState.walking;
            }

            animator.SetInteger("PlayerState", (int)playerState);
        }

        if (yAxis > 0)
        {
            lookDirection = LookDirection.up;
        }
        else if (yAxis < 0)
        {
            lookDirection = LookDirection.down;
        }
        else if (xAxis > 0)
        {
            lookDirection = LookDirection.right;
        }
        else if (xAxis < 0)
        {
            lookDirection = LookDirection.left;
        }

        animator.SetFloat("LookDir", (float)lookDirection);
    }

    private void UseItemSlot()
    {
        if (playerState != PlayerState.dead)
        {
            if (Input.GetButtonDown("Fire1"))
            {

            }

            if (Input.GetButtonDown("Fire2") == true && swordPickedUp == true)
            {
                playerState = PlayerState.attacking;
                animator.SetInteger("PlayerState", (int)playerState);
            }
        }
    }

    public void Hit()
    {
        StartCoroutine(FlashSprite(0.1f));
    }

    IEnumerator FlashSprite(float flashTime)
    {
        for (int i = 0; i < flashColors.Length; i++)
        {
            playerRenderer.material.color = flashColors[i];
            yield return new WaitForSeconds(flashTime);
        }
    }
    public void SetPlayerState(PlayerState state)
    {
        playerState = state;
        animator.SetInteger("PlayerState", (int)playerState);
    }

    public void TriggerDeathAnimation()
    {
        playerState = PlayerState.dead;
        animator.SetTrigger("Dying");
        StartCoroutine(FlashSprite(0.25f));
        animator.SetInteger("PlayerState", (int)playerState);
        gameManager.coroutine = StartCoroutine(gameManager.Restart());
    }

    public LookDirection GetDirection()
    {
        return lookDirection;
    }
}
