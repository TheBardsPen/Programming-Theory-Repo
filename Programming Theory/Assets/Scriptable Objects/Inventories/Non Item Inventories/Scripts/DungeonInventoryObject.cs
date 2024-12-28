using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dungeon Inventory", menuName = "Data Manager/Dungeon Inventory")]
public class DungeonInventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    private DungeonDatabaseObject database;
    public List<DungeonSlot> container = new List<DungeonSlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (DungeonDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Dungeon Database.asset", typeof(DungeonDatabaseObject));
#else
        database = Resources.Load<TownDatabaseObject>("Dungeon Database");
#endif
    }

    public void AddDungeon(DungeonObject dungeon)
    {
        container.Add(new DungeonSlot(database.GetId[dungeon], dungeon));
    }

    public void Save(string savePath)
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load(string savePath)
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < container.Count; i++)
        {
            container[i].dungeon = database.GetDungeon[container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {

    }
}

[System.Serializable]
public class DungeonSlot
{
    public int ID;
    public DungeonObject dungeon;

    public DungeonSlot(int _id, DungeonObject _dungeon)
    {
        ID = _id;
        dungeon = _dungeon;
    }
}