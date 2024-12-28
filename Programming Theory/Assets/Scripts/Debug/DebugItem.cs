using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;

public class DebugItem : MonoBehaviour
{
    public InventoryObject inventory;
    private ItemDataBaseObject database;
    public TMP_Dropdown itemDropdown;

    void Start()
    {
        itemDropdown.ClearOptions();

        List<string> strings = new List<string>();
        for (int i = 0; i < database.items.Length; i++)
        {
            strings.Add(database.items[i].name);
        }

        itemDropdown.AddOptions(strings);
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDataBaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Item Database.asset", typeof(ItemDataBaseObject));
#else
        database = Resources.Load<ItemDataBaseObject>("Item Database");
#endif
    }

    public void AddItem()
    {
        print("Tried to add " + itemDropdown.options[itemDropdown.value].text);
        inventory.AddItem(database.getItem[itemDropdown.value], 1);
    }
}
