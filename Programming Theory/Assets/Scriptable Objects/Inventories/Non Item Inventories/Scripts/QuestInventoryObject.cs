using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Inventory", menuName = "Data Manager/Quest Inventory")]
public class QuestInventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public List<QuestSlot> container = new List<QuestSlot>();

    public void AddQuest(QuestObject _quest, string _giver)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].giver == _giver)
            {
                // CODE TO REJECT ADDING QUEST GOES HERE//
                return;
            }
        }
        container.Add(new QuestSlot(_quest, _giver));
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
public class QuestSlot
{
    public QuestObject quest;
    public string giver;
    public QuestSlot(QuestObject _quest, string _giver)
    {
        quest = _quest;
        giver = _giver;
    }
}