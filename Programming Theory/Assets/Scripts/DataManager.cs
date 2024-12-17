using UnityEngine;
using System.IO;

/// <summary>
/// Handles all data saving between scenes and sessions
/// </summary>

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        // Variables for saving

    }

    public void AutoSave()
    {
        // Code for autosaving data

    }

    public void PreferencesSave()
    {
        // Code for saving user preferences set in options menu

    }

    public void Save(int index)
    {
        // Code for manual saving data

    }

    public void Load(int index)
    {
        // Code for manual loading data

    }

    public void PreferencesLoad()
    {
        // Code for autoloading user preferences on session start

    }
}
