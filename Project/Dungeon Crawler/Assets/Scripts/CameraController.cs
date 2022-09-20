using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera renderCamera;

    [SerializeField] private GameObject player;
    PlayerMovement playerMovement;

    [SerializeField] private float duration;
    [SerializeField] private Rect screenBounds = new Rect(0, 0, 1, 0.7f);

    private Coroutine currentCoroutine;
    [SerializeField] private float xWorldTileDistance = 16;
    [SerializeField] private float yWorldTileDistance = 11;
    [SerializeField] private float xPlayerTileDistance = 0;
    [SerializeField] private float yPlayerTileDistance = 1.5f;

    GameManager gameManager;

    void Start()
    {
        renderCamera = GetComponent<Camera>();

        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(currentCoroutine == null)
        {
            Vector3 viewPortPosition = renderCamera.WorldToViewportPoint(player.transform.position);

            if (viewPortPosition.x > screenBounds.width)
            {
                currentCoroutine = StartCoroutine(MoveToNextRoom(Vector3.right, xWorldTileDistance, xPlayerTileDistance));
            }
            else if (viewPortPosition.x < -screenBounds.x)
            {
                currentCoroutine = StartCoroutine(MoveToNextRoom(Vector3.left, xWorldTileDistance, xPlayerTileDistance));
            }
            else if (viewPortPosition.y > screenBounds.height)
            {
                currentCoroutine = StartCoroutine(MoveToNextRoom(Vector3.up, yWorldTileDistance, yPlayerTileDistance));
            }
            else if (viewPortPosition.y < screenBounds.y)
            {
                currentCoroutine = StartCoroutine(MoveToNextRoom(Vector3.down, yWorldTileDistance, yPlayerTileDistance));
            }
        }
    }


    public IEnumerator MoveToNextRoom(Vector3 direction, float camTileDistance, float playerTileDistance)
    {      
        gameManager.DeactivateCurrentRoom();
        yield return new WaitForSeconds(0.2f);
        Vector3 cameraStartPos = transform.position;
        Vector3 cameraEndPos = cameraStartPos + (direction * camTileDistance);

        Vector3 playerStartPos = player.transform.position;
        Vector3 playerEndPos = playerStartPos + (direction * playerTileDistance);


        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(cameraStartPos, cameraEndPos, timer);
            player.transform.position = Vector3.Lerp(playerStartPos, playerEndPos, timer);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        gameManager.ActivateRoom(cameraEndPos);

        currentCoroutine = null;
    }

    public void MoveToRoom(Room room)
    {
        currentCoroutine = StartCoroutine(DoPortalTransition(room.bounds.center));
    }

    private IEnumerator DoPortalTransition(Vector2 targetPosition)
    {
        gameManager.DeactivateCurrentRoom();
        yield return new WaitForSeconds(0.2f);

        transform.position = new Vector3(targetPosition.x,targetPosition.y + 2,transform.position.z);

        yield return new WaitForSeconds(0.2f);
        gameManager.ActivateRoom(transform.position);
        currentCoroutine = null;
    }
}
