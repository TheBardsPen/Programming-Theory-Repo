using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayDungeons : MonoBehaviour
{
    public DungeonInventoryObject inventory;

    public GameObject splash;

    private List<GameObject> buttons;
    
    public List<DungeonSlot> sortedList = new List<DungeonSlot>();

    // Positions for dynamic list
    public int X_START;
    public int Y_START;
    public int X_SPACE_LIST;
    public int Y_SPACE_LIST;
    public int COLUMNS;

    //Dictionary<DungeonSlot, GameObject> townDisplay = new Dictionary<DungeonSlot, GameObject>();

    private void OnEnable()
    {
        CreateDisplay();
    }

    private void OnDisable()
    {
        RemoveDisplay();
    }

    void Update()
    {
        //UpdateDisplay()
    }

    public void CreateDisplay()
    {
        buttons = new List<GameObject>();

        List<DungeonSlot> sortedList = inventory.container.OrderBy(o => o.dungeon.level).ToList();
        for (int i = 0; i < sortedList.Count; i++)
        {
            var dungeon = sortedList[i].dungeon;
            var name = dungeon.name;
            var level = dungeon.level;
            var obj = Instantiate(dungeon.prefabButton, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = name + " LV " + level;
            obj.GetComponent<Button>().onClick.AddListener(delegate { GameUIHandler.instance.UpdateLocation(name, $"Lv {level} Dungeon"); });
            obj.GetComponent<Button>().onClick.AddListener(delegate { GameUIHandler.instance.SplashSelect(splash); });
            obj.GetComponent<Button>().onClick.AddListener(() => DataManager.instance.currentLocation = dungeon);
            obj.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene(name); });
            if (dungeon.name == GameUIHandler.instance.location.text)
            {
                obj.GetComponent<Button>().interactable = false;
            }
            buttons.Add(obj);
        }
    }

    private void RemoveDisplay()
    {
        foreach (var obj in buttons)
        {
            Destroy(obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_LIST * (i % COLUMNS)), Y_START + (-Y_SPACE_LIST * (i / COLUMNS)), 0);
    }
}
