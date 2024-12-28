using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Linq;

/// <summary>
/// Derived class of DataManager
/// Container for handling options menu value changes
/// Preferences get saved on menu exit from the DataManager base class
/// </summary>

public class PrefsContainer : DataManager
{
    public TMP_Dropdown resoDropdown;
    public Toggle fullscreen;
    public Toggle wFullscreen;
    public Toggle windowed;

    // Dictionary for screen resolution sizes
    private readonly Dictionary<string, int> resoDatabase = new Dictionary<string, int>()
    {
        { "640 x 360", 0 },
        { "854 x 480", 1 },
        { "1280 x 720", 2 },
        { "1920 x 1080", 3 }
    };

    private void Start()
    {
        if (resoDropdown != null)
        {
            resoDropdown.ClearOptions();
            resoDropdown.AddOptions(resoDatabase.Keys.ToList());

            resoDropdown.onValueChanged.AddListener(ResolutionValueChanged);
        }
        fullscreen.onValueChanged.AddListener(ScreenFillChanged);
        wFullscreen.onValueChanged.AddListener(ScreenFillChanged);
        windowed.onValueChanged.AddListener(ScreenFillChanged);
    }

    private void ResolutionValueChanged(int index)
    {
        // Code to change screen resolution if windowed is selected
        int value = resoDatabase.Values.ElementAt(index);

        Debug.Log($"index {value} was selected");
    }

    private void ScreenFillChanged(bool isOn)
    {
        // Code to change screen fill type. enables resolution drop down if windowed is selected
        Debug.Log($"fullscreen :{fullscreen.isOn} - wfullscreen :{wFullscreen.isOn} - windowed :{windowed.isOn}");

        resoDropdown.interactable = windowed.isOn;
    }
}
