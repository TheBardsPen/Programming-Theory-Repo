using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dungeon Database", menuName = "Locations/Dungeon Database")]
public class DungeonDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public DungeonObject[] Dungeons;
    public Dictionary<DungeonObject, int> GetId = new Dictionary<DungeonObject, int>();
    public Dictionary<int, DungeonObject> GetDungeon = new Dictionary<int, DungeonObject>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<DungeonObject, int>();
        GetDungeon = new Dictionary<int, DungeonObject>();

        for (int i = 0; i < Dungeons.Length; i++)
        {
            GetId.Add(Dungeons[i], i);
            GetDungeon.Add(i, Dungeons[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}