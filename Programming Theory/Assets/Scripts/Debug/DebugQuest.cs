using UnityEngine;
using UnityEditor;
using TMPro;
using System.Collections.Generic;

public class DebugQuest : MonoBehaviour
{
    public MainQuestInventoryObject mainInventory;
    private MainQuestDatabaseObject mainDatabase;

    public SideQuestInventoryObject sideInventory;

    public TMP_Dropdown mainQuestInput;
    [SerializeField] TMP_Dropdown typeInput;
    [SerializeField] TMP_InputField giverInput;
    [SerializeField] TMP_InputField targetInput;
    [SerializeField] TMP_InputField countInput;
    [SerializeField] TMP_InputField rewardInput;

    private void Start()
    {
        mainQuestInput.ClearOptions();

        List<string> strings = new List<string>();

        for (int i = 0; i < mainDatabase.Quests.Length; i++)
        {
            strings.Add(mainDatabase.Quests[i].name);
        }

        mainQuestInput.AddOptions(strings);
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        mainDatabase = (MainQuestDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Main Quest Database.asset", typeof(MainQuestDatabaseObject));
#else
        database = Resources.Load<TownDatabaseObject>("Main Quest Database");
#endif
    }

    public void AddMainQuest()
    {
        mainInventory.AddQuest(mainDatabase.Quests[mainQuestInput.value]);
    }

    public void AddSideQuest()
    {
        sideInventory.AddSideQuest(typeInput.options[typeInput.value].text, giverInput.text, targetInput.text, int.Parse(countInput.text), int.Parse(rewardInput.text));
    }

    public void UpdateQuest()
    {
        foreach (SideQuestInventoryObject.SideQuestSlot slot in sideInventory.container)
        {
            if (slot.targetName == targetInput.text)
            {
                slot.currentCount += int.Parse(countInput.text);
                if (slot.currentCount >= slot.targetCount)
                {
                    slot.metRequirement = true;
                }
            }
        }
    }

    public void CompleteQuest()
    {
        for (int i = 0; i < sideInventory.container.Count; i++)
        {
            if (sideInventory.container[i].giver == giverInput.text)
            {
                SideQuestInventoryObject.SideQuestSlot slot = sideInventory.container[i];
                if (slot.metRequirement)
                {
                    print($"You finished a quest and made {slot.goldReward} gold!");
                    sideInventory.container.Remove(slot);
                }
                else
                {
                    print($"Sorry, you still need {slot.targetCount - slot.currentCount} {slot.targetName}s to finish this quest.");
                }
            }
        }
    }
}
