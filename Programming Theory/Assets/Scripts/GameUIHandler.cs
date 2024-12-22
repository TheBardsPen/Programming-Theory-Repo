using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

/// <summary>
/// Handles user interaction on the game screen
/// </summary>

public class GameUIHandler : MonoBehaviour
{
    public static GameUIHandler instance;
    public PlayerObject player;
    public TownInventoryObject townInventory;
    public DungeonInventoryObject dungeonInventory;
    public QuestInventoryObject questInventory;

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
        //location.text = DataManager.instance.location;
        //subLocation.text = DataManager.instance.subLocation;

        // Set player stats info
        playerName.text = player.container.name;
        statsDisplay.text =
            $"{player.container.level}\r\n" +
            $"{player.container.playerClass}\r\n\r\n" +
            $"{player.container.maxHealth}\r\n" +
            $"{player.container.maxMana}\r\n\r\n" +
            $"{player.container.strength}\r\n" +
            $"{player.container.dexterity}\r\n" +
            $"{player.container.constitution}\r\n" +
            $"{player.container.intelligence}\r\n" +
            $"{player.container.wisdom}\r\n" +
            $"{player.container.charisma}\r\n";

        // Set HP & MP gui
        hpText.text = $"{player.container.health}/{player.container.maxHealth}";
        mpText.text = $"{player.container.mana}/{player.container.maxMana}";
        hpBar.size = player.container.health / player.container.maxHealth;
        mpBar.size = player.container.mana / player.container.maxMana;
    }

    public void SplashSelect(GameObject splash)
    {
        // Handles splash menus opening and closing and remembers what side panel was open to reopen
        if (!splash.activeSelf)
        {           
            mainSplash.SetActive(false);
            splash.SetActive(true);
        }
        else if (splash.activeSelf)
        {           
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

    public void DungeonSelect(int index)
    {
        //DataManager.instance.location = dungeonButtons[index].GetComponentInChildren<TextMeshProUGUI>().text;
        //DataManager.instance.subLocation = null;
    }

    

    public void DisplayTownSubLocations(int townIndex)
    {
        
    }

    public void TownSelect(int index)
    {
        //DataManager.instance.location = townSelected;
        //DataManager.instance.subLocation = townSubButtons[index].GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void QuestList()
    {
        
    }

    public void SaveGame(int saveIndex)
    {
        DataManager.instance.Save(saveIndex);
    }

    public void ExitConfirm()
    {
        // Opens and closes exit game confirm panel
        if (!exitConfirm.activeSelf)
        {
            exitConfirm.SetActive(true);
            
        }
        else if (exitConfirm.activeSelf)
        {
            exitConfirm.SetActive(false);
            
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
}
