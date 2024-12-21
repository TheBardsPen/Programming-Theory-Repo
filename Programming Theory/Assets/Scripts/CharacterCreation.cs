using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public GameObject classScreen;
    public GameObject acceptScreen;
    public GameObject statsScreen;

    public TextMeshProUGUI nameField;
    public TextMeshProUGUI statsField;

    public TMP_InputField nameInput;
    public TMP_Dropdown classInput;

    private List<string> _playerClassSelect = new List<string>();
    private List<DataManager.Player> playerClassSelect = new List<DataManager.Player>
    {
        new DataManager.Player()
        {
            playerClass = "Warrior",
            level = 1,
            maxHealth = 100,
            health = 100,
            maxMana = 10,
            mana = 10,
            strength = 10,
            dexterity = 8,
            constitution = 10,
            intelligence = 6,
            wisdom = 4,
            charisma = 6,
        },

        new DataManager.Player()
        {
            playerClass = "Rogue",
            level = 1,
            maxHealth = 80,
            health = 80,
            maxMana = 12,
            mana = 12,
            strength = 7,
            dexterity = 10,
            constitution = 8,
            intelligence = 7,
            wisdom = 5,
            charisma = 8,
        },

        new DataManager.Player()
        {
            playerClass = "Mage",
            level = 1,
            maxHealth = 60,
            health = 60,
            maxMana = 20,
            mana = 20,
            strength = 4,
            dexterity = 6,
            constitution = 5,
            intelligence = 8,
            wisdom = 10,
            charisma = 7,
        }
    };

    private void Awake()
    {
        classInput.ClearOptions();

        for (int i = 0; i < playerClassSelect.Count; i++)
        {
            _playerClassSelect.Add(playerClassSelect[i].playerClass);
        }

        classInput.AddOptions(_playerClassSelect);
    }

    public void NameSubmit()
    {
        DataManager.instance.player.name = nameInput.text;
        if (classScreen.GetComponent<CanvasGroup>().alpha == 0)
        {
            classScreen.GetComponent<FadeObject>().FadeInObject();
        }
    }

    public void ClassSelect()
    {
        DataManager.instance.player = playerClassSelect[classInput.value];
        DataManager.instance.player.name = nameInput.text;
        if (acceptScreen.GetComponent<CanvasGroup>().alpha == 0)
        {
            statsScreen.GetComponent<FadeObject>().FadeInObject();
            acceptScreen.GetComponent<FadeObject>().FadeInObject();
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene("Town");
    }

    private void Update()
    {
        nameField.text = DataManager.instance.player.name;
        statsField.text =
            $"{DataManager.instance.player.level}\r\n" +
            $"{DataManager.instance.player.playerClass}\r\n\r\n" +
            $"{DataManager.instance.player.maxHealth}\r\n" +
            $"{DataManager.instance.player.maxMana}\r\n\r\n" +
            $"{DataManager.instance.player.strength}\r\n" +
            $"{DataManager.instance.player.dexterity}\r\n" +
            $"{DataManager.instance.player.constitution}\r\n" +
            $"{DataManager.instance.player.intelligence}\r\n" +
            $"{DataManager.instance.player.wisdom}\r\n" +
            $"{DataManager.instance.player.charisma}\r\n";
    }
}
