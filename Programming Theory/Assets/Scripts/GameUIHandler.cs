using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Hierarchy;
using TMPro;
using System;

/// <summary>
/// Handles user interaction on the game screen
/// </summary>

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] GameObject mainSplash;
    [SerializeField] GameObject exitConfirm;

    [SerializeField] List<GameObject> dungeonButtons = new List<GameObject>();

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
        for (int i = 0; i < DataManager.instance.availableDungeons.Count; i++)
        {
            dungeonButtons[i].SetActive(true);
            dungeonButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = DataManager.instance.availableDungeons[i].ToString();
        }
    }

    // degub code
    //
    //
    //
    public void DiscoverDungeon()
    {
        DataManager.instance.availableDungeons.Add("New Dungeon");
    }

    public void ExitConfirm()
    {
        // Opens exit game confirm

    }
}
