using UnityEngine;

[CreateAssetMenu(fileName = "Slayer Quest Object", menuName = "Quest/Slayer Quest")]
public class SlayerQuestObject : QuestObject
{
    private void Awake()
    {
        type = QuestType.Slayer;
    }
}
