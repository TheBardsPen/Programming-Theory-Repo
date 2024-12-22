using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayDungeons : MonoBehaviour
{
    public DungeonInventoryObject inventory;

    public int X_START;
    public int Y_START;
    public int X_SPACE_LIST;
    public int Y_SPACE_LIST;
    public int COLUMNS;
    //Dictionary<DungeonSlot, GameObject> townDisplay = new Dictionary<DungeonSlot, GameObject>();

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
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].location.name;
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_LIST * (i % COLUMNS)), Y_START + (-Y_SPACE_LIST * (i / COLUMNS)), 0);
    }
}
