using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
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
    public static GameUIHandler instance;

    [SerializeField] GameObject mainSplash;
    [SerializeField] GameObject exitConfirm;

    [SerializeField] TextMeshProUGUI location;
    [SerializeField] TextMeshProUGUI subLocation;

    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI statsDisplay;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI mpText;
    [SerializeField] Scrollbar hpBar;
    [SerializeField] Scrollbar mpBar;

    [SerializeField] List<GameObject> dungeonButtons = new List<GameObject>();
    [SerializeField] List<GameObject> townButtons = new List<GameObject>();
    [SerializeField] List<GameObject> townSubButtons = new List<GameObject>();
    [SerializeField] List<GameObject> questList = new List<GameObject>();

    private GameObject tempPanel;
    private string townSelected;

    void Awake()
    {
        // Code to handle UI toggles based on progress from load game

    }

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Handle all realtime updates

        // Set location and sublocation text
        location.text = DataManager.instance.location;
        subLocation.text = DataManager.instance.subLocation;

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

        // Set HP & MP gui
        hpText.text = $"{DataManager.instance.player.health}/{DataManager.instance.player.maxHealth}";
        mpText.text = $"{DataManager.instance.player.mana}/{DataManager.instance.player.maxMana}";
        hpBar.size = DataManager.instance.player.health / DataManager.instance.player.maxHealth;
        mpBar.size = DataManager.instance.player.mana / DataManager.instance.player.maxMana;
    }

    public void SplashSelect(GameObject splash)
    {
        // Handles splash menus opening and closing and remembers what side panel was open to reopen
        if (!splash.activeSelf)
        {
            if (GameObject.FindGameObjectsWithTag("Side Panel").Length > 0)
            {
                tempPanel = GameObject.FindGameObjectWithTag("Side Panel");
                tempPanel.SetActive(false);
            }            
            mainSplash.SetActive(false);
            splash.SetActive(true);
        }
        else if (splash.activeSelf)
        {
            if (tempPanel != null)
            {
                tempPanel.SetActive(true);
            }            
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

    public void DungeonSelect(int index)
    {
        DataManager.instance.location = dungeonButtons[index].GetComponentInChildren<TextMeshProUGUI>().text;
        DataManager.instance.subLocation = null;
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
        townSelected = DataManager.instance.availableTowns[townIndex].name;

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

    public void TownSelect(int index)
    {
        DataManager.instance.location = townSelected;
        DataManager.instance.subLocation = townSubButtons[index].GetComponentInChildren<TextMeshProUGUI>().text;
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
        SceneManager.LoadSceneAsync("Title");
        Destroy(gameObject);
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
        int count = new int();
        foreach (DataManager.Dungeon item in DataManager.instance.availableDungeons)
        {
            count++;
        }
        DataManager.instance.availableDungeons.Add(new DataManager.Dungeon { name = $"Dungeon {count}", level = 10 });
    }

    public void AddTown()
    {
        int count = new int();
        foreach (DataManager.Town item in DataManager.instance.availableTowns)
        {
            count++;
        }
        DataManager.instance.availableTowns.Add(new DataManager.Town { name = $"Town {count}", subs = new List<string>() });
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

    public void HealthChange(int amount)
    {
        DataManager.instance.player.health += amount;
    }

    public void ManaChange(int amount)
    {
        DataManager.instance.player.mana += amount;
    }
}
