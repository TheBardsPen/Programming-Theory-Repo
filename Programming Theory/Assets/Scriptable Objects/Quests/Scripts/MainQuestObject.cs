using UnityEngine;

[CreateAssetMenu(fileName = "Main Quest Object", menuName = "Quest/Main Quest")]
public class MainQuestObject : QuestObject
{
    private void Awake()
    {
        type = QuestType.Main;
    }
}