using UnityEditor;
using UnityEngine;

public enum QuestType
{
    Slayer,
    Harvest,
    Main
}

public abstract class QuestObject : ScriptableObject
{
    public GameObject prefabPanel;
    public QuestType type;
    public string giver;
    public string targetName;
    public int targetCount;
    public int currentCount;
    public int goldReward;
    [TextArea(15, 20)]
    public string description;
}
