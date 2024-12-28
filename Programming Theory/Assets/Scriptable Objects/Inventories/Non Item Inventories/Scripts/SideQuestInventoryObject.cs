using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "Side Quest Inventory", menuName = "Data Manager/Side Quest Inventory")]
public class SideQuestInventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public List<SideQuestSlot> container = new List<SideQuestSlot>();
    public GameObject questPanel;

    public void AddSideQuest(string _type, string _giver, string _targetName, int _targetCount, int _goldReward)
    {
        container.Add(new SideQuestSlot(questPanel, _type, _giver, _targetName, _targetCount, 0, _goldReward, false));
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

    [System.Serializable]
    public class SideQuestSlot
    {
        public GameObject questPanel;
        public string type;
        public string giver;
        public string targetName;
        public int targetCount;
        public int currentCount;
        public int goldReward;
        public bool metRequirement;

        public SideQuestSlot(GameObject _quest, string _type, string _giver, string _targetName, int _targetCount, int _currentCount, int _goldReward, bool _metRequirement)
        {
            questPanel = _quest;
            type = _type;
            giver = _giver;
            targetName = _targetName;
            targetCount = _targetCount;
            currentCount = _currentCount;
            goldReward = _goldReward;
            metRequirement = _metRequirement;
        }
    }
}
