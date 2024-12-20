using UnityEngine;
using UnityEngine.UI;
using System.IO;
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
        if (File.Exists(Application.persistentDataPath + "/autosave.json"))
        {
            continueButton.interactable = true;
            autosaveSplash.SetActive(true);
        }
    }

    public void NewGame()
    {

        // Code to launch character creation scene
        //SceneManager.LoadScene("CharacterCreation");

        //          //
        //          //
        //          //
        //   DEBUG  //
        //          //
        //          //
        //          //

        DataManager.instance.player.name = "Toby";
        DataManager.instance.player.level = 3;
        DataManager.instance.player.playerClass = "Warrior";
        DataManager.instance.player.health = 100;
        DataManager.instance.player.maxHealth = 100;
        DataManager.instance.player.mana = 20;
        DataManager.instance.player.maxMana = 20;
        DataManager.instance.player.strength = 9;
        DataManager.instance.player.dexterity = 3;
        DataManager.instance.player.constitution = 8;
        DataManager.instance.player.intelligence = 4;
        DataManager.instance.player.wisdom = 4;
        DataManager.instance.player.charisma = 5;

        SceneManager.LoadScene("Town");

    }

    public void Continue()
    {
        // Code to load autosave

    }

    public void LoadSaveFile()
    {
        // Code to open save menu to pick load data
        if (!loadGameSplash.activeSelf)
        {
            loadGameSplash.SetActive(true);
        }
        else
        {
            loadGameSplash.SetActive(false);
        }
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
}
