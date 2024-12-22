using UnityEngine;

[CreateAssetMenu(fileName = "Harvest Quest Object", menuName = "Quest/Harvest Quest")]
public class HarvestQuestObject : QuestObject
{
    private void Awake()
    {
        type = QuestType.Harvest;
    }
}