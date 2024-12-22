using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public PlayerObject player;

    public GameObject classScreen;
    public GameObject acceptScreen;
    public GameObject statsScreen;

    public TextMeshProUGUI nameField;
    public TextMeshProUGUI statsField;

    public TMP_InputField nameInput;
    public TMP_Dropdown classInput;

    private List<string> _playerClassSelect = new List<string>();
    private List<Player> playerClassSelect = new List<Player>
    {
        new Player(null, 1, "Warrior", 100, 100, 10, 10, 10, 8, 10, 6, 4, 6, 0),

        new Player(null, 1, "Rogue", 80, 80, 12, 12, 7, 10, 8, 7, 5, 8, 0),

        new Player(null, 1, "Mage", 60, 60, 20, 20, 4, 6, 5, 8, 10, 7, 0)
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
        player.container.name = nameInput.text;
        if (classScreen.GetComponent<CanvasGroup>().alpha == 0)
        {
            classScreen.GetComponent<FadeObject>().FadeInObject();
        }
    }

    public void ClassSelect()
    {
        player.container = playerClassSelect[classInput.value];
        player.container.name = nameInput.text;
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
        nameField.text = player.container.name;
        statsField.text =
            $"{player.container.level}\r\n" +
            $"{player.container.playerClass}\r\n\r\n" +
            $"{player.container.maxHealth}\r\n" +
            $"{player.container.maxMana}\r\n\r\n" +
            $"{player.container.strength}\r\n" +
            $"{player.container.dexterity}\r\n" +
            $"{player.container.constitution}\r\n" +
            $"{player.container.intelligence}\r\n" +
            $"{player.container.wisdom}\r\n" +
            $"{player.container.charisma}\r\n";
    }
}
