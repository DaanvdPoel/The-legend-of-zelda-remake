using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octorok : Enemy
{
    [SerializeField] private int maxDistance;
    [SerializeField] private int minDistance = 1;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float chanceToShoot;
    [SerializeField] public float damageAmount;

    private void Awake()
    {
        EnemyStateOnAwake();
    }

    protected override void Start()
    {
        base.Start();
        SetNextDestination();
    }

    private void SetNextDestination()
    {
        direction = (LookDirection)Random.Range(0, 4);

        Vector2 directionVector = DirectionToVector2(direction);

        int randomDistance = Random.Range(minDistance, maxDistance + 1);

        Vector2 destination = rb.position + directionVector * randomDistance;

        RaycastHit2D hit = Physics2D.Raycast(rb.position, directionVector, randomDistance, layerMask);

        if (hit.collider != null)
        {
            float distanceToHit = Vector2.Distance(rb.position, hit.point);

            if(distanceToHit >= 1)
            {
                float newDistance = distanceToHit - distanceToHit % 1;
                destination = rb.position + directionVector * newDistance;
            }
            else
            {
                destination = rb.position;
            }
        }

        if (!IsInsideViewport(destination))
        {
            destination = rb.position;
        }

          SetDestination(destination);
    }

    protected override void ReachedDestination()
    {
        base.ReachedDestination();

        if(Random.value < chanceToShoot)
        {
            Attack();
        }
        else
        {
            Invoke("SetNextDestination", timePerTile);
        }
    }

    protected override void Attack()
    {
        base.Attack();

        direction = CalculateDirection(rb.position, player.position);

        Invoke("ShootProjectile", Time.deltaTime * 20);
        Invoke("SetNextDestination", Time.deltaTime * 40);
    }

    public void ShootProjectile()
    {
        if(projectile == null)
        {
            Debug.LogError("No projectile set on " + name);
            return;
        }

        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);

        Projectile p = proj.GetComponent<Projectile>();

        if(p == null)
        {
            Debug.LogError("Projectile not fount on Instantiated instance " + name);
            return;
        }

        p.Launch(gameObject, direction);
    }

}
