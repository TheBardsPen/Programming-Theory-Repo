using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Object", menuName = "Locations/Dungeon")]
public class DungeonObject : LocationObject
{
    public int enemyLevel;

    private void Awake()
    {
        type = LocationType.Dungeon;
    }
}
