using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float timeTillDestroy = 5f;
    [SerializeField] public float damageAmount;

    private LookDirection direction;
    private Vector3 directionVector;
    private float aliveTime;

    public void Launch(GameObject owner, LookDirection direction)
    {
        SetDirection(direction);
        Physics2D.IgnoreCollision(owner.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        GetComponent<Rigidbody2D>().AddForce(directionVector * projectileSpeed, ForceMode2D.Impulse);
    }

    private void SetDirection(LookDirection lookDirection)
    {
        direction = lookDirection;

        switch (direction)
        {
            case LookDirection.up:
                directionVector = Vector3.up;
                break;
            case LookDirection.right:
                directionVector = Vector3.right;
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                break;
            case LookDirection.down:
                directionVector = Vector3.down;
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                break;
            case LookDirection.left:
                directionVector = Vector3.left;
                transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    void Update()
    {
        aliveTime += Time.deltaTime;
        if(aliveTime > timeTillDestroy)
        {
            Destroy(gameObject);
        }
    }
}
