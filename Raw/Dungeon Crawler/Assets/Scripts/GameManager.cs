using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor;
public class GameManager : MonoBehaviour
{
    [SerializeField] private int roomsAmountHorizontal;
    [SerializeField] private int roomsAmountVertical;
    [SerializeField] private Vector2 roomSize;
    [SerializeField] private Vector2 roomOffset;

    [SerializeField] private GameObject youDiedScreen;
    [SerializeField] private GameObject youWinScreen;
    [SerializeField] public AudioSource overworldMusicSource;
    [SerializeField] public AudioSource caveMusicSource;
    [SerializeField] public AudioSource sfxSource;
    [SerializeField] public AudioClip[] sfxClips;

    private Room[,] rooms;
    private Room currentRoom;

    public Coroutine coroutine;

    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Life life;
    [SerializeField] private Equipment equipment;
    [SerializeField] private GameObject brigeBlocker;
    [SerializeField] private GameObject brigeRender;

    private bool isBrigeActive = false;

    [SerializeField] private int extraHeartCost = 5;

void Start()
    {
        overworldMusicSource.Play();

        BuildRooms();

        RegisterSpawners();

        currentRoom = GetRoomWithWorldPosition(playerMovement.transform.position);

        if(currentRoom != null)
        {
            currentRoom.Activate(true);
        }
    }

    private void BuildRooms()
    {
        rooms = new Room[roomsAmountVertical, roomsAmountHorizontal];

        for (int row = 0; row < rooms.GetLength(0); row++)
        {
            for (int column = 0; column < rooms.GetLength(1); column++)
            {
                Vector2Int identifier = new Vector2Int(column, row);

                Vector2 centerOfRoom = new Vector2(column * roomSize.x, row * roomSize.y);

                centerOfRoom += roomOffset;

                rooms[row, column] = new Room(identifier, centerOfRoom, roomSize);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (rooms == null) return;

        Gizmos.color = Color.magenta;

        foreach(Room room in rooms)
        {
            Gizmos.DrawWireCube(room.bounds.center, room.bounds.size);

            if (Application.isEditor)
            {
                //Handles.Label(room.bounds.center, room.identifier.ToString());
            }
        }
    }

    private void RegisterSpawner(EnemySpawner spawner)
    {
        foreach(Room room in rooms)
        {
            room.RegisterIfInside(spawner);
        }
    }

    private void RegisterSpawners()
    {
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();

        foreach(EnemySpawner spawner in spawners)
        {
            RegisterSpawner(spawner);
        }
    }

    private Room GetRoomWithWorldPosition(Vector2 position)
    {
        Room room = null;

        foreach(Room r in rooms)
        {
            if (r.IsPositionInside(position))
            {
                room = r;

                break;
            }
        }


        return room;
    }

    public void ActivateRoom(Vector2 position)
    {
        Room room = GetRoomWithWorldPosition(position);

        if(room != null)
        {
            currentRoom = room;
            currentRoom.Activate(true);
        }
    }

    public void DeactivateCurrentRoom()
    {
        if(currentRoom != null)
        {
            currentRoom.Activate(false);
        }
    }

    public void TeleportPlayerToPortal(Portal portal)
    {
        Room room = (GetRoomWithWorldPosition(portal.transform.position));
        cameraController.MoveToRoom(room);
        portal.ReceiveObject(playerMovement.transform);
    }

    public IEnumerator Restart()
    {
        Debug.Log("restarting in 6 seconds");
        yield return new WaitForSeconds(1);
        overworldMusicSource.Stop();
        caveMusicSource.Stop();
        sfxSource.PlayOneShot(sfxClips[2]);
        youDiedScreen.SetActive(true);
        yield return new WaitForSeconds(5);

        Debug.Log("restart");
        SceneManager.LoadScene("MainMenu");
        coroutine = null;
    }

    public void OpenBrige()
    {
        if (equipment.keyCount >= 1f && isBrigeActive == false)
        {
            equipment.keyCount = equipment.keyCount - 1;
            brigeBlocker.SetActive(false);
            brigeRender.SetActive(true);
            isBrigeActive = true;
        }
    }

    public void StartBossFight()
    {
        overworldMusicSource.Stop();
        caveMusicSource.PlayDelayed(0.2f);
    }

    public IEnumerator YouWin()
    {
        Debug.Log("Going to Main menu in 6 seconds");
        yield return new WaitForSeconds(1);
        overworldMusicSource.Stop();
        caveMusicSource.Stop();
        sfxSource.PlayOneShot(sfxClips[3]);
        youWinScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        Debug.Log("loading menu");
        SceneManager.LoadScene("MainMenu");
        coroutine = null;
    }

    public void MoreMaxHealth()
    {
        if(equipment.rupeeCount >= extraHeartCost)
        {
            equipment.rupeeCount = equipment.rupeeCount - extraHeartCost;
            playerHealth.AddMoreMaxHealth(1);
            life.AddExtraHeartToUI();
            playerHealth.ChangeHealth(1f);
            life.HeartsUpdate();
        }
        else
        {
            Debug.Log("je hebt niet genoeg geld");
        }
    }
} 
