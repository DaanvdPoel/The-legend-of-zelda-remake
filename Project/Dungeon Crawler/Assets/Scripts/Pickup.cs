using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject[] pickups;
    private List<GameObject> pickupsSpawned;

    private void Start()
    {
        pickupsSpawned = new List<GameObject>();
    }

    public virtual void SpawnPickup(Vector3 spawnLocation)
    {
        int random = Random.Range(0, pickups.Length);
        if(pickups[random] != null)
        {
            GameObject go = Instantiate(pickups[random], spawnLocation, Quaternion.identity);
            pickupsSpawned.Add(go);
        }
    }

    public void RemoveAllPickups()
    {
        for (int i = 0; i < pickupsSpawned.Count; i++)
        {
            Destroy(pickupsSpawned[i]);
        }

        pickupsSpawned.Clear();
    }

    protected virtual void IfPickedUp()
    {
        GameManager gameManager;
        gameManager = FindObjectOfType<GameManager>();

        gameManager.overworldMusicSource.Stop();
        gameManager.sfxSource.PlayOneShot(gameManager.sfxClips[0]);
        gameManager.overworldMusicSource.PlayDelayed(1f);
        Destroy(gameObject);
    }
}
