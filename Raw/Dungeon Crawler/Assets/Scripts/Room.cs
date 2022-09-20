using System.Collections.Generic;
using UnityEngine;
public class Room
{
    public Vector2Int identifier;
    public Rect bounds;
    private List<EnemySpawner> spawners;

    public Room(Vector2Int identifier, Vector2 center, Vector2 size)
    {
        this.identifier = identifier;

        spawners = new List<EnemySpawner>();

        bounds = new Rect();
        bounds.size = size;
        bounds.center = center;
    }

    public void Activate(bool shouldActivate)
    {
        foreach (EnemySpawner spawner in spawners)
        {
            spawner.enabled = shouldActivate;
        }
    }

    public void RegisterIfInside(EnemySpawner spawner)
    {
        if (bounds.Contains(spawner.transform.position) == true)
        {
            spawners.Add(spawner);
        }
    }

    public bool IsPositionInside(Vector2 position)
    {
        return bounds.Contains(position);
    }
}
