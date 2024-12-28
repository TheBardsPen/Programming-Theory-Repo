using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Main Quest", menuName = "Quest/Main Quest")]
public class MainQuestObject : ScriptableObject
{
    public GameObject prefabPanel;
    public int goldReward;
    [TextArea(15, 10)]
    public string description;
}
