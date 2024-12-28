using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTowns : MonoBehaviour
{
    public TownInventoryObject inventory;

    public static DisplayTowns instance;

    public GameObject splash;

    public string townSelected; // Allows update of gui

    public List<GameObject> buttons = new List<GameObject>();
    public List<GameObject> subButtons = new List<GameObject>();

    // Positions for dynamic list
    public int X_START_TOWN;
    public int X_START_SUB;
    public int Y_START;
    public int X_SPACE_LIST;
    public int Y_SPACE_LIST;
    public int COLUMNS;

    private void OnEnable()
    {
        // Create accesible instance for sublocation display
        instance = this;

        CreateDisplay();
    }

    private void OnDisable()
    {
        // Remove all objects from scene to allow update on reopen and prevent memory leak
        foreach (GameObject obj in buttons)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in subButtons)
        {
            Destroy(obj);
        }

        townSelected = null;
    }

    void Update()
    {
        //UpdateDisplay()
    }

    public void CreateDisplay()
    {
        // Empty local lists
        buttons.Clear();
        subButtons.Clear();

        // Create list of available towns from player data
        for (int i = 0; i < inventory.container.Count; i++)
        {
            GameObject obj = Instantiate(inventory.container[i].town.prefabButton, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetTownPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].town.name;
            obj.AddComponent<DisplaySubLocations>();
            buttons.Add(obj);
        }
    }

    public void EmptySubList()
    {
        // Remove current sublocations when selecting new town
        foreach (GameObject obj in subButtons)
        {
            Destroy(obj);
        }
    }

    public Vector3 GetTownPosition(int i)
    {
        return new Vector3(X_START_TOWN + (X_SPACE_LIST * (i % COLUMNS)), Y_START + (-Y_SPACE_LIST * (i / COLUMNS)), 0);
    }
}
