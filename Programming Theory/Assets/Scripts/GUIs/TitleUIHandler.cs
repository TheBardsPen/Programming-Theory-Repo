using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Handles title menu UI interactions
/// </summary>

public class TitleUIHandler : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] GameObject autosaveSplash;
    [SerializeField] GameObject optionsSplash;
    [SerializeField] GameObject loadGameSplash;

    private void Start()
    {
        // Code to check for autosave file and enable continue button / character stats splash
        if (DataManager.instance.hasAutoSave)
        {
            continueButton.interactable = true;
            autosaveSplash.SetActive(true);
        }
    }

    public void NewGame()
    {
        // Code to launch character creation scene
        SceneManager.LoadScene("CharacterCreation");
    }

    public void Continue()
    {
        // Code to launch game with autoload data

    }

    public void LoadSaveFile()
    {
        // Code to open save menu to pick load data
        //if (!loadGameSplash.activeSelf)
        //{
        //    loadGameSplash.SetActive(true);
        //}
        //else
        //{
        //    loadGameSplash.SetActive(false);
        //}
    }

    public void OptionsMenu()
    {
        // Code to open and close options interface
        if (!optionsSplash.activeSelf)
        {
            optionsSplash.SetActive(true);
        }
        else
        {
            optionsSplash.SetActive(false);
        }

    }

    public void ExitGame()
    {
        // Code to exit game
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    //
    //
    // DEBUG
    //
    //

    public void DebugLoad()
    {
        DataManager.instance.Load("Debug");
        SceneManager.LoadScene("Test Town Inn");
    }
}
