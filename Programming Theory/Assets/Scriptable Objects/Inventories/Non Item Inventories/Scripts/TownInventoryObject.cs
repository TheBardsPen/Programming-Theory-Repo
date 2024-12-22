using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Town Inventory", menuName = "Data Manager/Town Inventory")]
public class TownInventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public List<TownSlot> container = new List<TownSlot>();

    public void AddTown(TownObject _town, string _sub)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].location == _town)
            {
                container[i].AddSublocation(_sub);
                return;
            }
        }
        container.Add(new TownSlot(_town, new List<string>() { _sub }));
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
public class TownSlot
{
    public LocationObject location;
    public List<string> subLocations = new List<string>();

    public TownSlot(LocationObject _location, List<string> _subLocations)
    {
        location = _location;
        subLocations = _subLocations;
    }

    public void AddSublocation(string loc)
    {
        subLocations.Add(loc);
    }
}