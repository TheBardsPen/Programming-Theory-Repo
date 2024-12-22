using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTowns : MonoBehaviour
{
    public TownInventoryObject inventory;

    public int X_START_TOWN;
    public int X_START_SUB;
    public int Y_START;
    public int X_SPACE_LIST;
    public int Y_SPACE_LIST;
    public int COLUMNS;
    //Dictionary<TownSlot, GameObject> townDisplay = new Dictionary<TownSlot, GameObject>();

    private void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateDisplay()
    }

    public void CreateDisplay()
    {
        // Creates list of available towns from the save file active
        // Deacctivates all available sub location buttons on panel open
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].location.prefabButton, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetTownPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].location.name;
            obj.GetComponent<Button>().onClick.AddListener(delegate { CreateSubDisplay(i); });
        }
    }

    public void CreateSubDisplay(int i)
    {
        foreach (string sub in inventory.container[i].subLocations)
        {
            List<string> strings = inventory.container[i].subLocations;
            var obj = Instantiate(inventory.container[i].location.prefabButton, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetSubPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = sub;
        }
    }

    public Vector3 GetTownPosition(int i)
    {
        return new Vector3(X_START_TOWN + (X_SPACE_LIST * (i % COLUMNS)), Y_START + (-Y_SPACE_LIST * (i / COLUMNS)), 0);
    }

    public Vector3 GetSubPosition(int i)
    {
        return new Vector3(X_START_SUB + (X_SPACE_LIST * (i % COLUMNS)), Y_START + (-Y_SPACE_LIST * (i / COLUMNS)), 0);
    }
}
