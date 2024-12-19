using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Hierarchy;
using TMPro;
using System;
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

    [SerializeField] List<GameObject> dungeonButtons = new List<GameObject>();
    [SerializeField] List<GameObject> townButtons = new List<GameObject>();
    [SerializeField] List<GameObject> townSubButtons = new List<GameObject>();

    void Awake()
    {
        // Code to handle UI toggles based on progress from load game

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
        if (sidePanel.activeSelf)
        {
            return;
        }
        else if (!sidePanel.activeSelf) 
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Side Panel"))
            {
                item.SetActive(false);
            }
            sidePanel.SetActive(true);
        } 
    }

    public void DungeonList()
    {
        // Creates list of available dungeons to the save file active
        for (int i = 0; i < DataManager.instance.availableDungeons.Count; i++)
        {
            dungeonButtons[i].SetActive(true);
            dungeonButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = DataManager.instance.availableDungeons[i].name;
        }
    }

    public void TownList()
    {
        // Creates list of available towns to the save file active
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

    // debug code
    //
    //
    //
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
}
