using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dungeon Inventory", menuName = "Data Manager/Dungeon Inventory")]
public class DungeonInventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public List<DungeonSlot> container = new List<DungeonSlot>();

    public void AddDungeon(DungeonObject location)
    {
        container.Add(new DungeonSlot(location));
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

    }

    public void OnBeforeSerialize()
    {

    }
}

[System.Serializable]
public class DungeonSlot
{
    public LocationObject location;

    public DungeonSlot(LocationObject _location)
    {
        location = _location;
    }
}