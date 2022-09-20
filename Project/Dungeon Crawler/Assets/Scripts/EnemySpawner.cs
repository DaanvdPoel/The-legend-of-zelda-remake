using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    redOctorok,
    blueOctorok
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyType[] enemytype;
    [SerializeField] private float interval = 1;

    private float spawnTimer;
    private List<GameObject> spawnedEnemies;

    private SpriteRenderer spriterenderer;

    private void Awake()
    {
        spawnedEnemies = new List<GameObject>();
        spriterenderer = GetComponent<SpriteRenderer>();

        enabled = false;
    }
    void Update()
    {
        if(spawnedEnemies.Count == enemytype.Length)
        {
            return;
        }

        spawnTimer = spawnTimer + Time.deltaTime;

        if(spawnTimer >= interval)
        {
            SpawnNextEnemy();
            spawnTimer = 0;
        }
    }

    private void SpawnNextEnemy()
    {
        string enemyPathName = "";

        switch (enemytype[spawnedEnemies.Count])
        {
            case EnemyType.redOctorok:
                enemyPathName = "Prefabs/Enemys/Red Octorok";
                break;
            case EnemyType.blueOctorok:
                enemyPathName = "Prefabs/Enemys/Blue Octorok";
                break;
        }

        GameObject go = Instantiate(Resources.Load<GameObject>(enemyPathName), transform.position, Quaternion.identity);
        spawnedEnemies.Add(go);
    }

    private void OnDisable()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            Destroy(spawnedEnemies[i]);
        }

        spawnedEnemies.Clear();
        spawnTimer = 0;
        spriterenderer.material.color = Color.red;
    }

    private void OnEnable()
    {
        spriterenderer.material.color = Color.white;
    }
}
