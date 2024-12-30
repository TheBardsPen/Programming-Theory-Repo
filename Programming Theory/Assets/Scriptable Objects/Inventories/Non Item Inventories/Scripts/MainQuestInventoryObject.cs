using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Main Quest Inventory", menuName = "Data Manager/Main Quest Inventory")]
public class MainQuestInventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    private MainQuestDatabaseObject database;
    public List<MainQuestSlot> container = new List<MainQuestSlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (MainQuestDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Main Quest Database.asset", typeof(MainQuestDatabaseObject));
#else
        database = Resources.Load<MainQuestDatabaseObject>("Main Quest Database");
#endif
    }

    public void AddQuest(MainQuestObject _quest)
    {
        container.Add(new MainQuestSlot(database.GetId[_quest], _quest, false));
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
            container[i].quest = database.GetQuest[container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {

    }
}

[System.Serializable]
public class MainQuestSlot
{
    public int ID;
    public MainQuestObject quest;
    public bool metRequirement;
    public MainQuestSlot(int _id, MainQuestObject _quest, bool _metRequirement)
    {
        ID = _id;
        quest = _quest;
        metRequirement = _metRequirement;
    }
}