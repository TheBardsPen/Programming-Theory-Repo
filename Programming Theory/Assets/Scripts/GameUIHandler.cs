using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Hierarchy;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Handles user interaction on the game screen
/// </summary>

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] GameObject mainSplash;
    [SerializeField] GameObject exitConfirm;
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI statsDisplay;

    [SerializeField] List<GameObject> dungeonButtons = new List<GameObject>();
    [SerializeField] List<GameObject> townButtons = new List<GameObject>();
    [SerializeField] List<GameObject> townSubButtons = new List<GameObject>();
    [SerializeField] List<GameObject> questList = new List<GameObject>();

    private GameObject tempPanel;

    void Awake()
    {
        // Code to handle UI toggles based on progress from load game

    }

    private void Update()
    {
        // Handle all realtime updates

        // Set player stats info
        playerName.text = DataManager.instance.player.name;
        statsDisplay.text =
            $"{DataManager.instance.player.level}\r\n" +
            $"{DataManager.instance.player.playerClass}\r\n\r\n" +
            $"{DataManager.instance.player.maxHealth}\r\n" +
            $"{DataManager.instance.player.maxMana}\r\n\r\n" +
            $"{DataManager.instance.player.strength}\r\n" +
            $"{DataManager.instance.player.dexterity}\r\n" +
            $"{DataManager.instance.player.constitution}\r\n" +
            $"{DataManager.instance.player.intelligence}\r\n" +
            $"{DataManager.instance.player.wisdom}\r\n" +
            $"{DataManager.instance.player.charisma}\r\n";

    }

    public void SplashSelect(GameObject splash)
    {

        // Handles splash menus opening and closing and remembers what side panel was open to reopen
        if (!splash.activeSelf)
        {
            tempPanel = GameObject.FindGameObjectWithTag("Side Panel");
            tempPanel.SetActive(false);
            mainSplash.SetActive(false);
            splash.SetActive(true);
        }
        else if (splash.activeSelf)
        {
            tempPanel.SetActive(true);
            mainSplash.SetActive(true);
            splash.SetActive(false);
        }
    }

    public void SidePanelSelect(GameObject sidePanel)
    {
        // Handles side panel selection
        if (!sidePanel.activeSelf)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Side Panel"))
            {
                item.SetActive(false);
            }
            sidePanel.SetActive(true);
        }
        else if (sidePanel.activeSelf) 
        {
            
        } 
    }

    public void DungeonList()
    {
        // Creates list of available dungeons from the save file active
        for (int i = 0; i < DataManager.instance.availableDungeons.Count; i++)
        {
            dungeonButtons[i].SetActive(true);
            dungeonButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = DataManager.instance.availableDungeons[i].name;
        }
    }

    public void TownList()
    {
        // Creates list of available towns from the save file active
        // Deacctivates all available sub location buttons on panel open
        foreach (GameObject item in townSubButtons)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < DataManager.instance.availableTowns.Count; i++)
        {
            townButtons[i].SetActive(true);
            townButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = DataManager.instance.availableTowns[i].name;
        }
    }

    public void DisplayTownSubLocations(int townIndex)
    {
        // Creates list of available sub locations in each town
        foreach (GameObject item in townSubButtons)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < DataManager.instance.availableTowns[townIndex].subs.Count; i++)
        {
            townSubButtons[i].SetActive(true);
            townSubButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = DataManager.instance.availableTowns[townIndex].subs[i];
        }
    }

    public void QuestList()
    {
        foreach (GameObject item in questList)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < DataManager.instance.availableQuests.Count; i++)
        {
            questList[i].SetActive(true);
            questList[i].GetComponentInChildren<TextMeshProUGUI>().text =
                $"{DataManager.instance.availableQuests[i].type}\r\n" +
                $"{DataManager.instance.availableQuests[i].giver}\r\n" +
                $"{DataManager.instance.availableQuests[i].target}\r\n" +
                $"{DataManager.instance.availableQuests[i].acquireCount}/{DataManager.instance.availableQuests[i].targetCount}";
        }
    }

    public void ExitConfirm()
    {
        // Opens and closes exit game confirm panel
        if (!exitConfirm.activeSelf)
        {
            exitConfirm.SetActive(true);
            foreach (GameObject item in townSubButtons)
            {
                item.GetComponent<Button>().interactable = false;
            }
            foreach (GameObject item in townButtons)
            {
                item.GetComponent<Button>().interactable = false;
            }
            foreach (GameObject item in dungeonButtons)
            {
                item.GetComponent<Button>().interactable = false;
            }
            foreach (Button item in GameObject.Find("Main Buttons").GetComponentsInChildren<Button>())
            {
                item.interactable = false;
            }
        }
        else if (exitConfirm.activeSelf)
        {
            exitConfirm.SetActive(false);
            foreach (GameObject item in townSubButtons)
            {
                item.GetComponent<Button>().interactable = true;
            }
            foreach (GameObject item in townButtons)
            {
                item.GetComponent<Button>().interactable = true;
            }
            foreach (GameObject item in dungeonButtons)
            {
                item.GetComponent<Button>().interactable = true;
            }
            foreach (Button item in GameObject.Find("Main Buttons").GetComponentsInChildren<Button>())
            {
                item.interactable = true;
            }
        }
    }

    public void ExitGame()
    {
        // Code to exit game
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
    }

    //          //
    //          //
    //          //
    //   DEBUG  //
    //          //
    //          //
    //          //

    public void DiscoverDungeon()
    {
        DataManager.instance.availableDungeons.Add(new DataManager.Dungeon { name = "New Dungeon", level = 10 });
    }

    public void AddTown()
    {
        DataManager.instance.availableTowns.Add(new DataManager.Town { name = "New Town", subs = new List<string> { "City Center", "Hotel" } });
    }

    public void AddSubLocation()
    {
        DataManager.instance.availableTowns[0].subs.Add("New Sub");
    }

    public void AddQuestMonster()
    {
        DataManager.instance.availableQuests.Add(new DataManager.Quest 
        { 
            type = "Bounty",
            giver = "Tom",
            target = "goblins",
            targetCount = Random.Range(6, 12),
            gold = Random.Range(50, 100)
        });
    }

    public void AddQuestItem()
    {
        DataManager.instance.availableQuests.Add(new DataManager.Quest
        {
            type = "Harvest",
            giver = "Carol",
            target = "Bear Skin",
            targetCount = Random.Range(6, 12),
            gold = Random.Range(50, 100)
        });
    }

    public void StatAssign()
    {
        DataManager.instance.player.name = "Toby";
        DataManager.instance.player.level = 3;
        DataManager.instance.player.playerClass = "Warrior";
        DataManager.instance.player.maxHealth = 100;
        DataManager.instance.player.maxMana = 20;
        DataManager.instance.player.strength = 9;
        DataManager.instance.player.dexterity = 3;
        DataManager.instance.player.constitution = 8;
        DataManager.instance.player.intelligence = 4;
        DataManager.instance.player.wisdom = 4;
        DataManager.instance.player.charisma = 5;
    }
}
