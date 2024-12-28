using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEditor.Search;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.PackageManager.UI;

[CreateAssetMenu(fileName = "New Player", menuName = "Data Manager/Player")]
public class PlayerObject : ScriptableObject, ISerializationCallbackReceiver
{
    public Player container = new Player(null, 1, 0, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

    public void SetClass(string className, string playerName)
    {
        if (className == "warrior")
        {
            container = new Player(playerName, 1, 0, "Warrior", 100, 100, 10, 10, 10, 8, 10, 6, 4, 6, 0, 0);
        }
        if (className == "rogue")
        {
            container = new Player(playerName, 1, 0, "Rogue", 80, 80, 12, 12, 7, 10, 8, 7, 5, 8, 0, 0);
        }
        if (className == "mage")
        {
            container = new Player(playerName, 1, 0, "Mage", 60, 60, 20, 20, 4, 6, 5, 8, 10, 7, 0, 0);
        }
    }

    public void Save(string savePath)
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load(string savePath)
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
        
    }
}

[System.Serializable]
public class Player
{
    public string name;
    public int level;
    public int exp;
    public string playerClass;
    public float health;
    public float maxHealth;
    public float mana;
    public float maxMana;
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;
    public int statPoints;
    public int gold;

    public Player(string _name, int _level, int _exp, string _playerClass, float _health, float _maxHealth, float _mana, float _maxMana, int _strength, int _dexterity, int _constitution, int _intelligence, int _wisdom, int _charisma, int _statPoints, int _gold)
    {
        name = _name;
        level = _level;
        exp = _exp;
        playerClass = _playerClass;
        health = _health;
        maxHealth = _maxHealth;
        mana = _mana;
        maxMana = _mana;
        strength = _strength;
        dexterity = _dexterity;
        constitution = _constitution;
        intelligence = _intelligence;
        wisdom = _wisdom;
        charisma = _charisma;
        statPoints = _statPoints;
        gold = _gold;
    }
}