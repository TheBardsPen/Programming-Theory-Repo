using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class DisplayQuestList : MonoBehaviour
{
    public MainQuestInventoryObject mainInventory;
    public SideQuestInventoryObject sideQuestInventory;

    public GameObject questPanel;

    private Button button;
    private int siblingIndex;
    private string giverName;

    private bool areQuestsShown;

    private List<GameObject> questByGiver = new List<GameObject>();

    private void Start()
    {
        // Set local variables on creation of button
        giverName = gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        button = gameObject.GetComponent<Button>();

        CreateDisplay();
    }

    private void CreateDisplay()
    {
        // Get index in sibling hierarchy
        siblingIndex = gameObject.transform.GetSiblingIndex();

        if (giverName == "Main Quests")
        {
            for (int i = 0; i < mainInventory.container.Count; i++)
            {
                var mainQuest = Instantiate(questPanel, transform);
                mainQuest.transform.SetParent(DisplayQuests.instance.grid.transform);
                mainQuest.transform.SetSiblingIndex(0);
                mainQuest.transform.SetSiblingIndex(siblingIndex + 1);
                mainQuest.name = $"{giverName} {i}";
                mainQuest.SetActive(false);
                mainQuest.GetComponentInChildren<TextMeshProUGUI>().text = mainInventory.container[i].quest.description;
                questByGiver.Add(mainQuest);
                DisplayQuests.instance.subPanels.Add(mainQuest);
            }
        }
        else
        {
            for (int i = 0; i < sideQuestInventory.container.Count; i++)
            {
                if (sideQuestInventory.container[i].giver == giverName)
                {
                    var sideQuest = Instantiate(questPanel, transform);
                    sideQuest.transform.SetParent(DisplayQuests.instance.grid.transform);
                    sideQuest.transform.SetSiblingIndex(0);
                    sideQuest.transform.SetSiblingIndex(siblingIndex + 1);
                    sideQuest.name = $"{giverName} {i}";
                    sideQuest.SetActive(false);
                    questByGiver.Add(sideQuest);
                    if (sideQuestInventory.container[i].metRequirement)
                    {
                        sideQuest.GetComponentInChildren<TextMeshProUGUI>().text =
                        $"{sideQuestInventory.container[i].type}\r\n" +
                        $"{sideQuestInventory.container[i].targetName}\r\n" +
                        $"{sideQuestInventory.container[i].currentCount}/{sideQuestInventory.container[i].targetCount}\r\n" +
                        $"Worth {sideQuestInventory.container[i].goldReward} gold\r\n" +
                        $"Complete!";
                    }
                    else
                    {
                        sideQuest.GetComponentInChildren<TextMeshProUGUI>().text =
                        $"{sideQuestInventory.container[i].type}\r\n" +
                        $"{sideQuestInventory.container[i].targetName}\r\n" +
                        $"{sideQuestInventory.container[i].currentCount}/{sideQuestInventory.container[i].targetCount}\r\n" +
                        $"Worth {sideQuestInventory.container[i].goldReward} gold\r\n" +
                        $"Not finished";
                    }
                    DisplayQuests.instance.subPanels.Add(sideQuest);
                }
            }
        }
        button.onClick.AddListener(ShowDisplay);
        areQuestsShown = false;
    }

    private void ShowDisplay()
    {
        // Enables quest objects when giver name is clicked
        if (!areQuestsShown)
        {
            foreach (GameObject obj in questByGiver)
            {
                if (obj.name.Contains(giverName))
                {
                    obj.SetActive(true);
                }
            }
            areQuestsShown = true;
        }
        else
        {
            foreach (GameObject obj in questByGiver)
            {
                if (obj.name.Contains(giverName))
                {
                    obj.SetActive(false);
                }
            }
            areQuestsShown = false;
        }
    }
}
