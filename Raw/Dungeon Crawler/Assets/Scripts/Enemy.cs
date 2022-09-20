using UnityEngine;

public enum EnemyState
{
    idle,
    roaming,
    attacking,
    dead,
    spawning,
    lowHP
}

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float timePerTile = 1f;
    protected Rigidbody2D rb;
    private EnemyState enemyState;
    protected LookDirection direction;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float distanceToLerp;
    private float lerpTimer;

    private Camera renderCam;

    private Animator animator;

    protected Transform player;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>().transform;
        renderCam = GameObject.Find("RenderCam").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        UpdateAnimator();

        if (enemyState == EnemyState.roaming)
        {
            MoveToDestination();
        }
    }

    protected void SetDestination(Vector3 destination)
    {
        startPosition = transform.position;
        targetPosition = destination;

        distanceToLerp = Vector2.Distance(startPosition, destination);

        lerpTimer = 0;

        enemyState = EnemyState.roaming;
    }

    private void MoveToDestination()
    {
        lerpTimer += Time.deltaTime;

        if (lerpTimer > (timePerTile * distanceToLerp))
        {
            lerpTimer = (timePerTile * distanceToLerp);
        }

        if (!distanceToLerp.Equals(0f) && !timePerTile.Equals(0f))
        {
            float perc = lerpTimer / (timePerTile * distanceToLerp);
            rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, perc));
        }

        if (lerpTimer.Equals(timePerTile * distanceToLerp))
        {
            ReachedDestination();
        }
    }

    protected virtual void ReachedDestination()
    {
        enemyState = EnemyState.idle;
    }

    protected Vector2 DirectionToVector2(LookDirection direction)
    {
        Vector2 directionVector = Vector2.zero;
        switch (direction)
        {
            case LookDirection.up:
                directionVector = Vector2.up;
                break;
            case LookDirection.left:
                directionVector = Vector2.left;
                break;
            case LookDirection.right:
                directionVector = Vector2.right;
                break;
            case LookDirection.down:
                directionVector = Vector2.down;
                break;
        }
        return directionVector;
    }
    protected bool IsInsideViewport(Vector2 position)
    {
        Vector2 viewportPoint = renderCam.WorldToViewportPoint(position);
        return (viewportPoint.x > 0f && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 0.7f);
    }

    private void UpdateAnimator()
    {
        if(animator != null)
        {
            animator.SetInteger("State", (int)enemyState);
            animator.SetFloat("LookDir", (float)direction);
        }
        else
        {
            Debug.LogError("No animator found on " + name);
        }
    }

    protected virtual void Attack()
    {
        enemyState = EnemyState.attacking;
    }

    protected float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

    protected LookDirection CalculateDirection(Vector2 pos1, Vector2 pos2, int amountdirections = 4)
    {
        float angle = 360f - CalculateAngle(pos1, pos2);

        float part = 360f / amountdirections;

        if (angle.Equals(360))
        {
            angle = 0;
        }

        int result = Mathf.RoundToInt(angle / part);

        if(result == amountdirections)
        {
            result = 0;
        }

        LookDirection direction = LookDirection.up;

        switch (result)
        {
            case 1:
                direction = LookDirection.right;
                break;
            case 2:
                direction = LookDirection.down;
                break;
            case 3:
                direction = LookDirection.left;
                break;
        }

        return direction;
    }

    protected void EnemyStateOnAwake()
    {
        enemyState = EnemyState.spawning;       
    }
}
