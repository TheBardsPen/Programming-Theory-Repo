using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Main Quest Database", menuName = "Quest/Main Quest Database")]
public class MainQuestDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public MainQuestObject[] Quests;
    public Dictionary<MainQuestObject, int> GetId = new Dictionary<MainQuestObject, int>();
    public Dictionary<int, MainQuestObject> GetQuest = new Dictionary<int, MainQuestObject>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<MainQuestObject, int>();
        GetQuest = new Dictionary<int, MainQuestObject>();

        for (int i = 0; i < Quests.Length; i++)
        {
            GetId.Add(Quests[i], i);
            GetQuest.Add(i, Quests[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
