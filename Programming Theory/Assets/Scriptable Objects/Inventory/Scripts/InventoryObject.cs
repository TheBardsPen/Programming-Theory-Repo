using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDataBaseObject database;
    public List<InventorySlot> container = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDataBaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDataBaseObject));
#else
        database = Resources.Load<ItemDataBaseObject>("Database");
#endif
    }

    public void AddItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == _item)
            {
                container[i].AddAmount(_amount);
                return;
            }
        }
            container.Add(new InventorySlot(database.getId[_item], _item, _amount));
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.dataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(string.Concat(Application.dataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.dataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < container.Count; i++)
            container[i].item = database.getItem[container[i].ID];
    }

    public void OnBeforeSerialize()
    {

    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}