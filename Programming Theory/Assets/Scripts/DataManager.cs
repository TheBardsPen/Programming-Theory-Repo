using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;

/// <summary>
/// Handles all data saving between scenes and sessions
/// </summary>

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public class Player
    {
        public string name;
        public int level;
        public string playerClass;
        public int health;
        public int maxHealth;
        public int mana;
        public int maxMana;
        public int strength;
        public int dexterity;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;
    }

    public Player player = new Player();

    public struct Town
    {
        public string name;
        public List<string> subs;
    }
    public struct Dungeon
    {
        public string name;
        public int level;
    }

    public struct Quest
    {
        public string type;
        public string giver;
        public string target;
        public int targetCount;
        public int acquireCount;
        public int gold;
    }
    
    public List<Town> availableTowns = new List<Town>();
    public List<Dungeon> availableDungeons = new List<Dungeon>();
    public List<Quest> availableQuests = new List<Quest>();

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

    [System.Serializable]
    class SaveData
    {
        // Variables for saving

    }

    public void AutoSave()
    {
        // Code for autosaving data

    }

    public void PreferencesSave()
    {
        // Code for saving user preferences set in options menu

    }

    public void Save(int index)
    {
        // Code for manual saving data

    }

    public void Load(int index)
    {
        // Code for manual loading data

    }

    public void PreferencesLoad()
    {
        // Code for autoloading user preferences on session start

    }
}
