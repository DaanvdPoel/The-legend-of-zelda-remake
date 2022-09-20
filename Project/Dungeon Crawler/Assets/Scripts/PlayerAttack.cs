using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject[] sword;
    [SerializeField] private Vector3[] boxOffset;
    [SerializeField] private Vector3[] boxSize;
    [SerializeField] private LayerMask layerToHit;

    private LookDirection lookDir;
    private PlayerAnimation playerAnim;

    private void Start()
    {
        playerAnim = GetComponent<PlayerAnimation>(); 
    }
    void Update()
    {
        SwingSword();
    }

    private void SwingSword()
    {
        if (Input.GetButtonDown("Fire2") && playerAnim.swordPickedUp == true && playerAnim.playerState != PlayerState.dead)
        {
            lookDir = playerAnim.GetDirection();

            TurnSwordOff();
            sword[(int)lookDir].SetActive(true);
            Invoke("TurnSwordOff", 0.2f);

            RaycastHit2D hit2D;
            hit2D = Physics2D.BoxCast(transform.position + boxOffset[(int)lookDir], boxSize[(int)lookDir], 0, Vector3.zero, 0, layerToHit);
            if(hit2D.collider != null)
            {
                hit2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
            }
        }
    }

    private void TurnSwordOff()
    {
        sword[(int)lookDir].SetActive(false);
    }
}
