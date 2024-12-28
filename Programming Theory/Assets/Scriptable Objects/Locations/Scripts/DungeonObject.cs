using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Object", menuName = "Locations/Dungeon")]
public class DungeonObject : LocationObject
{
    public int level;

    private void Awake()
    {
        type = LocationType.Dungeon;
    }
}
