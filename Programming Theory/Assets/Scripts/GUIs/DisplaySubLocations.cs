using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplaySubLocations : MonoBehaviour
{
    private TownInventoryObject inventory;

    private GameObject splash;

    private Button button;
    private string townName;
    private int townIndex;

    public int X_START_SUB = 208;
    public int Y_START = 360;
    public int X_SPACE_LIST = 416;
    public int Y_SPACE_LIST = 80;
    public int COLUMNS = 1;

    private void Start()
    {
        // Grab town list from player data
        inventory = DataManager.instance.townInventory;

        // Set local variables on button creation
        button = GetComponent<Button>();
        button.onClick.AddListener(CreateDisplay);

        splash = DisplayTowns.instance.splash;

        townName = this.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    private void CreateDisplay()
    {
        // Check for current selected town
        if (townName != DisplayTowns.instance.townSelected)
        {
            // Remove all sublocations to prevent overlap
            foreach (GameObject obj in DisplayTowns.instance.subButtons)
            {
                Destroy(obj);
            }

            // Set town selected on click
            DisplayTowns.instance.townSelected = townName;

            // Get index of selected town from player data
            townIndex = inventory.container.FindIndex(a => a.town.name == townName);

            // Create buttons from indexed town
            for (int i = 0; i < inventory.container[townIndex].town.subLocations.Count; i++)
            {
                TownObject town = inventory.container[townIndex].town;
                string subName = town.subLocations[i];

                GameObject obj = Instantiate(town.prefabButton, Vector3.zero, Quaternion.identity, transform.parent);
                obj.GetComponent<RectTransform>().localPosition = GetSubPosition(i);

                // Check if sublocation has been discovered
                if (inventory.container[townIndex].subLocations.Contains(subName))
                {
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = subName;
                }
                else
                {
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = "?????";
                    obj.GetComponent<Button>().interactable = false;
                }

                // Add listeners
                obj.GetComponent<Button>().onClick.AddListener(delegate { GameUIHandler.instance.UpdateLocation(DisplayTowns.instance.townSelected, obj.GetComponentInChildren<TextMeshProUGUI>().text); });
                obj.GetComponent<Button>().onClick.AddListener(delegate { GameUIHandler.instance.SplashSelect(splash); });
                obj.GetComponent<Button>().onClick.AddListener(delegate { DataManager.instance.currentLocation = town; });
                obj.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene($"{townName} {subName}"));

                // Check if location is current
                if (town.subLocations[i] == GameUIHandler.instance.subLocation.text && town.name == GameUIHandler.instance.location.text)
                {
                    obj.GetComponent<Button>().interactable = false;
                }

                // Add to list for dynamic updating
                DisplayTowns.instance.subButtons.Add(obj);
            }
        }
    }

    public Vector3 GetSubPosition(int i)
    {
        return new Vector3(X_START_SUB + (X_SPACE_LIST * (i % COLUMNS)), Y_START + (-Y_SPACE_LIST * (i / COLUMNS)), 0);
    }
}
