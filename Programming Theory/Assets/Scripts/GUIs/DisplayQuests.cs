using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DisplayQuests : MonoBehaviour
{
    public static DisplayQuests instance;

    public MainQuestInventoryObject mainInventory;
    public SideQuestInventoryObject sideQuestInventory;

    public GameObject questGiverButton;
    public GameObject questPanel;
    public GameObject grid;


    private List<GameObject> panels = new List<GameObject>();
    public List<GameObject> subPanels = new List<GameObject>();

    private void OnEnable()
    {
        // Create accesible reference for quest lists
        instance = this;

        CreateDisplay();
    }

    private void OnDisable()
    {
        RemoveDisplay();
    }

    void Update()
    {
        //UpdateDisplay()
    }

    private void CreateDisplay()
    {
        panels.Clear();
        subPanels.Clear();
        // Bool check if main quest button should be been made
        bool hasMainQuest = false;
        if (mainInventory.container.Count > 0)
        {
            hasMainQuest = true;
        }

        // Create main quest button
        if (hasMainQuest)
        {
            var nameObj = Instantiate(questGiverButton, transform);
            nameObj.transform.SetParent(grid.transform);
            nameObj.transform.SetAsFirstSibling();
            nameObj.GetComponentInChildren<TextMeshProUGUI>().text = "Main Quests";
            panels.Add(nameObj);
        }

        // Side Quest checks
        List<string> giverNames = new List<string>();
        List<SideQuestInventoryObject.SideQuestSlot> sortedNames = sideQuestInventory.container.OrderBy(x => x.giver).ToList();
        for (int i = 0; i < sideQuestInventory.container.Count; i++)
        {
            if (!giverNames.Contains(sortedNames[i].giver))
            {
                var nameObj = Instantiate(questGiverButton, transform);
                nameObj.transform.SetParent(grid.transform);
                nameObj.GetComponentInChildren<TextMeshProUGUI>().text = sortedNames[i].giver;
                giverNames.Add(sortedNames[i].giver);
                panels.Add(nameObj);
            }
        }

        /*List<MainQuestSlot> sortedMainList = mainInventory.container.OrderBy(o => o.ID).ToList();

        // List for holding giver names from created name buttons
        List<string> giverNames = new List<string>();

        
        

        // Create buttons for each unique giver name in quest list
        for (int i = 0; i < sortedList.Count; i++)
        {
            // Check for non main quests(no description)
            if (sortedList[i].type != "Main")
            {
                // Check for uniqe names to avoid duplication and then create buttons
                if (!giverNames.Contains(sortedList[i].giver))
                {
                    var nameObj = Instantiate(questGiverButton, transform);
                    nameObj.transform.SetParent(grid.transform);
                    nameObj.GetComponentInChildren<TextMeshProUGUI>().text = sortedList[i].giver;
                    giverNames.Add(sortedList[i].giver);
                    panels.Add(nameObj);
                }
            }
            // Turn on main quest button activator
            else
            {
                hasMainQuest = true;
            }
        }*/

        
        
    }

    private void RemoveDisplay()
    {
        // Remove objects from scene to prevent duplication and memory leak
        foreach (GameObject obj in panels)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in subPanels)
        {
            Destroy(obj);
        }
    }
}
