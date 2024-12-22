using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all data saving between scenes and sessions
/// </summary>

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private string savePath;

    public PlayerObject player;
    public InventoryObject inventory;
    public TownInventoryObject townInventory;
    public DungeonInventoryObject dungeonInventory;
    public QuestInventoryObject questInventory;

    private void Awake()
    {
        savePath = $"{Application.dataPath}/Saves";
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
        questInventory.container.Clear();
        townInventory.container.Clear();
        dungeonInventory.container.Clear();
        player.container = null;
    }

    [System.Serializable]
    class SaveData
    {
        
    }

    public void AutoSave()
    {
        // Code for autosaving data

    }

    public void PreferencesSave()
    {
        // Code for saving user preferences set in options menu

    }

    public void Save(int saveIndex)
    {
        // Check for directory of index
        if (!Directory.Exists($"{savePath}/{saveIndex}"))
        {
            Directory.CreateDirectory($"{savePath}/{saveIndex}");
        }

        // Save inventory per player
        inventory.Save($"{savePath}/{saveIndex}/Inv");
    }

    public void Load(int saveIndex)
    {
        // Code for manual loading player data
        
        // Load inventory per player
        if (File.Exists($"{savePath}/{saveIndex}/Inv"))
        {
            inventory.Load($"{savePath}/{saveIndex}/Inv");
        }
        //
        //
        // Debug for loading
        //
        //
        SceneManager.LoadScene("Town");
    }

    public void PreferencesLoad()
    {
        // Code for autoloading user preferences on session start

    }
}
