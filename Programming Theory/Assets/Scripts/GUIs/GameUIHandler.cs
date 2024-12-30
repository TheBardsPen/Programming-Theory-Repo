using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Ink.Runtime;
using System.Linq;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Handles user interaction on the game screen
/// </summary>

public class GameUIHandler : MonoBehaviour
{
    public static GameUIHandler instance;

    // Player data containers
    public PlayerObject player;
    public TownInventoryObject townInventory;
    public DungeonInventoryObject dungeonInventory;
    public MainQuestInventoryObject mainQuestInventory;
    public SideQuestInventoryObject sideQuestInventory;

    // Gameobjects for main interactions
    [SerializeField] GameObject mainSplash;
    [SerializeField] GameObject exitConfirm;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject choicePanel;
    [SerializeField] GameObject[] defaultChoiceOptions;

    // Gameobjects for location display
    public TextMeshProUGUI location;
    public TextMeshProUGUI subLocation;

    // Gameobjects for player hotbar
    [SerializeField] GameObject playerHotbar;
    [SerializeField] GameObject battleLog;
    public TextMeshProUGUI battleText;
    private int maxLogLength = 10;
    private bool isLogLimitMet;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI mpText;
    [SerializeField] Scrollbar hpBar;
    [SerializeField] Scrollbar mpBar;

    // Variables to handle player data that need persistence across scenes and inactive objects
    private int expToLevelUp;

    //
    //
    //DEBUG
    //
    //
    public GameObject debugMenu;

    void Awake()
    {
        // Code to handle UI toggles based on progress from load game

    }

    private void Start()
    {
        // Create persistent UI across scenes. Will destroy UI on main menu return to prevent overlapping UIs
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        // Set undeclared variables
        battleText = battleLog.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Grab location from datamanager
        LocationObject currentLocation = DataManager.instance.currentLocation;

        // Update hotbar and dialogue box based on location type
        if (currentLocation != null)
        {
            if (currentLocation.type == LocationType.Town)
            {
                dialogueBox.SetActive(true);
                choicePanel.SetActive(true);
                playerHotbar.SetActive(false);
                battleLog.SetActive(false);
            }
            else
            {
                dialogueBox.SetActive(false);
                choicePanel.SetActive(false);
                playerHotbar.SetActive(true);
                battleLog.SetActive(true);
            }
        }

        // Clear oldest battle log line at max
        if (currentLocation != null && currentLocation.type == LocationType.Dungeon)
        {
            if (battleText.text.Split("\n").Length >= maxLogLength)
            {
                isLogLimitMet = true;
            }
        }
        if (isLogLimitMet)
        {
            StartCoroutine(TrimLogLines());
            isLogLimitMet = false;
        }

        // Watch for player level up
        expToLevelUp = (((player.container.level * 9) * (player.container.level * 7)) - player.container.exp);

        if (expToLevelUp <= 0)
        {
            PlayerLevelUp();
        }

        // Update HP & MP bars from player stats
        hpText.text = $"{player.container.health}/{player.container.maxHealth}";
        mpText.text = $"{player.container.mana}/{player.container.maxMana}";
        hpBar.size = player.container.health / player.container.maxHealth;
        mpBar.size = player.container.mana / player.container.maxMana;

        //
        //
        //DEBUG
        //
        //

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DebugMenu();
        }
    }

    public void SplashSelect(GameObject splash)
    {
        // Handles splash menus opening and closing
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
            // Close all active side panels to update their display properly
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Side Panel"))
            {
                item.SetActive(false);
            }

            sidePanel.SetActive(true);
        }
    }

    public void StartDialogue()
    {
        /*foreach (GameObject obj in defaultChoiceOptions)
        {
            obj.SetActive(false);
        }
        Story story = new Story(GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCController>().npc.story.text);
        story.variablesState["relationship"] = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCController>().relationship;
        story.variablesState["isQuestActive"] = GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCController>().isQuestActive;
        DialogueManager.instance.story = story;
        DialogueManager.instance.StartDialogue();*/
    }

    public void EndDialogue()
    {
        foreach (GameObject obj in defaultChoiceOptions)
        {
            obj.SetActive(true);
        }
    }

    private IEnumerator TrimLogLines()
    {
        string[] logLines = battleText.text.Split("\n").Skip(1).ToArray();
        string output = string.Join("\n", logLines);
        battleText.text = output;
        yield return null;
    }

    public void PlayerLevelUp()
    {
        // Handles player leveling up
        player.container.level++;
        player.container.maxHealth += Mathf.Round(player.container.constitution * 1.5f);
        player.container.health = player.container.maxHealth;
        player.container.maxMana += Mathf.Round(player.container.wisdom / 2);
        player.container.mana = player.container.maxMana;
        player.container.statPoints++;
        player.container.exp = 0;
    }

    public void UpdateLocation(string primary, string sub)
    {
        // Updates location display in gui when new is area selected
        location.text = primary;
        subLocation.text = sub;
    }

    public void Save()
    {
        DataManager.instance.Save(player.container.name);
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

    private void DebugMenu()
    {
        if (debugMenu.activeSelf)
        {
            debugMenu.SetActive(false);
        }
        else
        {
            debugMenu.SetActive(true);
        }
    }
}
