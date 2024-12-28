using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Rendering;
using System.Linq;
using System;
using System.Collections;

public class DebugTown : MonoBehaviour
{
    public TownInventoryObject inventory;
    private TownDatabaseObject database;
    public TMP_Dropdown townInput;
    public TMP_Dropdown subLocationInput;

    private void Start()
    {
        townInput.ClearOptions();

        List<string> townStrings = new List<string>();

        for (int i = 0; i < database.Towns.Length; i++)
        {
            townStrings.Add(database.Towns[i].name);
        }

        townInput.AddOptions(townStrings);
    }

    public void SubLocationUpdate()
    {
        List<string> subStrings = new List<string>();

        for (int i = 0;i < database.Towns[townInput.value].subLocations.Count; i++)
        {
            subStrings.Add(database.Towns[townInput.value].subLocations[i]);
        }

        subLocationInput.ClearOptions();
        subLocationInput.AddOptions(subStrings);
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (TownDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Town Database.asset", typeof(TownDatabaseObject));
#else
        database = Resources.Load<TownDatabaseObject>("Town Database");
#endif
    }

    public void AddTown()
    {
        inventory.AddTown(database.Towns[townInput.value], subLocationInput.options[subLocationInput.value].text);
    }
}
