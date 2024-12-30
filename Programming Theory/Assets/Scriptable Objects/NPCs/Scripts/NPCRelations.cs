using Ink.Parsed;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

[CreateAssetMenu(fileName = "NPC Relations", menuName = "Data Manager/NPC Relations")]
public class NPCRelations : ScriptableObject, ISerializationCallbackReceiver
{
    private NPCDatabase database;
    public List<NPCSlot> container = new List<NPCSlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (NPCDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPC Database.asset", typeof(NPCDatabase));
#else
        database = Resources.Load<NPCDatabase>("NPC Database");
#endif
    }

    public void AddNPC(NPCObject _npc)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].npc == _npc)
            {
                return;
            }
        }
        container.Add(new NPCSlot(database.getId[_npc], _npc, 0, false));
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
public class NPCSlot
{
    public int ID;
    public NPCObject npc;
    public int relationship;
    public bool isQuestActive;

    public NPCSlot(int _id, NPCObject _npc, int _relationship, bool _isQuestActive)
    {
        ID = _id;
        npc = _npc;
        relationship = _relationship;
        isQuestActive = _isQuestActive;
    }

    public void SetRelationship(int amount)
    {
        relationship = amount;
    }
}
