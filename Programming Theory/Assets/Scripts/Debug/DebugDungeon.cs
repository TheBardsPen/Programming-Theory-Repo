using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor;

public class DebugDungeon : MonoBehaviour
{
    public DungeonInventoryObject inventory;
    private DungeonDatabaseObject database;
    public TMP_Dropdown dungeonInput;

    private void Start()
    {
        dungeonInput.ClearOptions();


        List<string> strings = new List<string>();


        for (int i = 0; i < database.Dungeons.Length; i++)
        {
            strings.Add(database.Dungeons[i].name);
        }

        dungeonInput.AddOptions(strings);
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (DungeonDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Dungeon Database.asset", typeof(DungeonDatabaseObject));
#else
        database = Resources.Load<TownDatabaseObject>("Dungeon Database");
#endif
    }

    public void AddDungeon()
    {
        inventory.AddDungeon(database.Dungeons[dungeonInput.value]);
    }
}
