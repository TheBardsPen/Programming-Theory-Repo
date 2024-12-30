using Ink.Parsed;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Dungeon Object", menuName = "Locations/Dungeon")]
public class DungeonObject : LocationObject
{
    public int level;
    public List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        type = LocationType.Dungeon;
    }
}
