using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;

[CreateAssetMenu(fileName = "New Player", menuName = "Data Manager/Player")]
public class PlayerObject : ScriptableObject, ISerializationCallbackReceiver
{
    public Player container = new Player(null, 1, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

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

    public Player(string _name, int _level, string _playerClass, float _health, float _maxHealth, float _mana, float _maxMana, int _strength, int _dexterity, int _constitution, int _intelligence, int _wisdom, int _charisma, int _statPoints)
    {
        name = _name;
        level = _level;
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
    }
}