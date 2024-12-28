using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Town Inventory", menuName = "Data Manager/Town Inventory")]
public class TownInventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    private TownDatabaseObject database;
    public List<TownSlot> container = new List<TownSlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (TownDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Town Database.asset", typeof(TownDatabaseObject));
#else
        database = Resources.Load<TownDatabaseObject>("Town Database");
#endif
    }

    public void AddTown(TownObject _town, string _sub)
    {
        List<string> strings = new List<string>();

        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].town == _town)
            {
                container[i].AddSublocation(_sub);
                return;
            }
        }
        var obj = new TownSlot(database.GetId[_town], _town, _sub);
        container.Add(obj);
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
        for (int i = 0;i < container.Count; i++)
        {
            container[i].town = database.GetTown[container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {

    }
}

[System.Serializable]
public class TownSlot
{
    public int ID;
    public TownObject town;
    public List<string> subLocations = new List<string>();

    public TownSlot(int _id, TownObject _town, string _sub)
    {
        ID = _id;
        town = _town;
        AddSublocation(_sub);
    }

    public void AddSublocation(string _sub)
    {
        if (!subLocations.Contains(_sub))
        {
            subLocations.Add(_sub);
        }
    }
}