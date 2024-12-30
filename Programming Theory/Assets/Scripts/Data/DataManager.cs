using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

/// <summary>
/// Handles all data saving between scenes and sessions
/// </summary>

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public bool hasAutoSave = false;
    private string savePath;

    public PlayerObject player; // Player data container
    public InventoryObject inventory; // Item inventory container
    public TownInventoryObject townInventory; // Discovered town and sublocation list
    public DungeonInventoryObject dungeonInventory; // Discovered dungeon list
    public MainQuestInventoryObject mainQuestInventory; // Main quests currently active for the player
    public SideQuestInventoryObject sideQuestInventory; // Side quests currently active for the player
    public NPCRelations npcRelations; // NPC relationships for the active player

    public LocationObject currentLocation;

    private void Awake()
    {
        savePath = $"{Application.dataPath}/Saves";
        if (Directory.Exists($"{savePath}/Auto"))
        {
            hasAutoSave = true;
        }
    }

    void Start()
    {
        // Create persistent DataManager to allow saving and loading across all scenes during session
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        // Auto load autosave data to update title display
        // Load player data
        if (File.Exists($"{savePath}/Auto/Player"))
        {
            player.Load($"{savePath}/Auto/Player");
        }
        // Load inventory
        if (File.Exists($"{savePath}/Auto/Inv"))
        {
            inventory.Load($"{savePath}/Auto/Inv");
        }
        // Load towns
        if (File.Exists($"{savePath}/Auto/Town"))
        {
            inventory.Load($"{savePath}/Auto/Town");
        }
        // Load dungeons
        if (File.Exists($"{savePath}/Auto/Dungeon"))
        {
            inventory.Load($"{savePath}/Auto/Dungeon");
        }
        // Load active quests
        if (File.Exists($"{savePath}/Auto/Quest"))
        {
            inventory.Load($"{savePath}/Auto/Quest");
        }
    }

    private void OnApplicationQuit()
    {
        // Clear all containers on game exit to prevent duplication or save cross contamination
        inventory.container.Clear();
        mainQuestInventory.container.Clear();
        sideQuestInventory.container.Clear();
        townInventory.container.Clear();
        dungeonInventory.container.Clear();
        npcRelations.container.Clear();
        player.container = null;
    }

    [System.Serializable]
    class SaveData
    {
        
    }

    public void AutoSave()
    {
        // Check for directory of index and create new if necessary
        if (!Directory.Exists($"{savePath}/Auto"))
        {
            Directory.CreateDirectory($"{savePath}/Auto");
        }

        // Code for autosaving data

    }

    public void PreferencesSave()
    {
        // Code for saving user preferences set in options menu

    }

    public void Save(string saveIndex)
    {
        // Check for directory of index and create new if necessary
        if (!Directory.Exists($"{savePath}/{saveIndex}"))
        {
            Directory.CreateDirectory($"{savePath}/{saveIndex}");
        }

        // Save player data
        player.Save($"{savePath}/{saveIndex}/Player");
        // Save inventory
        inventory.Save($"{savePath}/{saveIndex}/Inv");
        // Save towns discovered
        townInventory.Save($"{savePath}/{saveIndex}/Town");
        // Save dungeons discovered
        dungeonInventory.Save($"{savePath}/{saveIndex}/Dungeon");
        // Save active main quest list
        mainQuestInventory.Save($"{savePath}/{saveIndex}/Quest");
        // Save active side quest list
        sideQuestInventory.Save($"{savePath}/{saveIndex}/Side Quest");
        // Save npc relations
        npcRelations.Save($"{savePath}/{saveIndex}/NPC");
    }

    public void Load(string saveIndex)
    {
        // Load player data
        if (File.Exists($"{savePath}/{saveIndex}/Player"))
        {
            player.Load($"{savePath}/{saveIndex}/Player");
        }
        // Load inventory
        if (File.Exists($"{savePath}/{saveIndex}/Inv"))
        {
            inventory.Load($"{savePath}/{saveIndex}/Inv");
        }
        // Load towns
        if (File.Exists($"{savePath}/{saveIndex}/Town"))
        {
            townInventory.Load($"{savePath}/{saveIndex}/Town");
        }
        // Load dungeons
        if (File.Exists($"{savePath}/{saveIndex}/Dungeon"))
        {
            dungeonInventory.Load($"{savePath}/{saveIndex}/Dungeon");
        }
        // Load active main quests
        if (File.Exists($"{savePath}/{saveIndex}/Quest"))
        {
            mainQuestInventory.Load($"{savePath}/{saveIndex}/Quest");
        }
        // Load active side quests
        if (File.Exists($"{savePath}/{saveIndex}/Side Quest"))
        {
            sideQuestInventory.Load($"{savePath}/{saveIndex}/Side Quest");
        }
        if (File.Exists($"{savePath}/{saveIndex}/NPC"))
        {
            npcRelations.Load($"{savePath}/{saveIndex}/NPC");
        }
    }

    public void PreferencesLoad()
    {
        // Code for autoloading user preferences on session start

    }
}
