using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class LevelUp : MonoBehaviour
{
    public PlayerObject player;

    public TextMeshProUGUI statPointsLeft;
    public TextMeshProUGUI stats;
    public GameObject commitButton;
    public List<Button> addPointButtons = new List<Button>();
    public List<Button> minusPointButtons = new List<Button>();

    public List<Action> addActions = new List<Action>();
    public List<Action> removeActions = new List<Action>();

    private int expToLevelUp;

    private int strengthAdded;
    private int dexterityAdded;
    private int constitutionAdded;
    private int intelligenceAdded;
    private int wisdomAdded;
    private int charismaAdded;

    private int uncommitedPoints;

    private void Start()
    {
        addActions.Add(AddStrength);
        addActions.Add(AddDexterity);
        addActions.Add(AddConstitution);
        addActions.Add(AddIntelligence);
        addActions.Add(AddWisdom);
        addActions.Add(AddCharisma);

        removeActions.Add(RemoveStrength);
        removeActions.Add(RemoveDexterity);
        removeActions.Add(RemoveConstitution);
        removeActions.Add(RemoveIntelligence);
        removeActions.Add(RemoveWisdom);
        removeActions.Add(RemoveCharisma);

        for (int i = 0; i < addPointButtons.Count; i++)
        {
            addPointButtons[i].onClick.AddListener(addActions[i].Invoke);
        }

        for (int i = 0; i < minusPointButtons.Count; i++)
        {
            minusPointButtons[i].onClick.AddListener(removeActions[i].Invoke);
        }
    }

    private void OnEnable()
    {
        strengthAdded = 0;
        dexterityAdded = 0;
        constitutionAdded = 0;
        intelligenceAdded = 0;
        wisdomAdded = 0;
        charismaAdded = 0;

        uncommitedPoints = 0;
    }

    private void OnDisable()
    {
        foreach (Button button in minusPointButtons)
        {
            button.gameObject.SetActive(false);
        }

        player.container.statPoints += uncommitedPoints;
    }

    private void LateUpdate()
    {
        expToLevelUp = (((player.container.level * 9) * (player.container.level * 7)) - player.container.exp);

        statPointsLeft.text = player.container.statPoints.ToString();

        stats.text =
            $"{player.container.strength + strengthAdded}\r\n" +
            $"{player.container.dexterity + dexterityAdded}\r\n" +
            $"{player.container.constitution + constitutionAdded}\r\n" +
            $"{player.container.intelligence + intelligenceAdded}\r\n" +
            $"{player.container.wisdom + wisdomAdded}\r\n" +
            $"{player.container.charisma + charismaAdded}\r\n\r\n" +
            $"{expToLevelUp}";

        if (player.container.statPoints == 0)
        {
            foreach (Button button in addPointButtons)
            {
                button.gameObject.SetActive(false);
            }
            commitButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            foreach (Button button in addPointButtons)
            {
                button.gameObject.SetActive(true);
            }
            commitButton.GetComponent<Button>().interactable = false;
        }
        ChangeCheck();
    }

    public void Commit()
    {
        uncommitedPoints = 0;
        player.container.strength += strengthAdded;
        player.container.dexterity += dexterityAdded;
        player.container.constitution += constitutionAdded;
        player.container.intelligence += intelligenceAdded;
        player.container.wisdom += wisdomAdded;
        player.container.charisma += charismaAdded;
        gameObject.SetActive(false);
    }

    private void ChangeCheck()
    {
        if (strengthAdded > 0)
        {
            minusPointButtons[0].gameObject.SetActive(true);
        }
        else
        {
            minusPointButtons[0].gameObject.SetActive(false);
        }

        if (dexterityAdded > 0)
        {
            minusPointButtons[1].gameObject.SetActive(true);
        }
        else
        {
            minusPointButtons[1].gameObject.SetActive(false);
        }

        if (constitutionAdded > 0)
        {
            minusPointButtons[2].gameObject.SetActive(true);
        }
        else
        {
            minusPointButtons[2].gameObject.SetActive(false);
        }

        if (intelligenceAdded > 0)
        {
            minusPointButtons[3].gameObject.SetActive(true);
        }
        else
        {
            minusPointButtons[3].gameObject.SetActive(false);
        }

        if (wisdomAdded > 0)
        {
            minusPointButtons[4].gameObject.SetActive(true);
        }
        else
        {
            minusPointButtons[4].gameObject.SetActive(false);
        }

        if (charismaAdded > 0)
        {
            minusPointButtons[5].gameObject.SetActive(true);
        }
        else
        {
            minusPointButtons[5].gameObject.SetActive(false);
        }
    }

    private void AddStrength()
    {
        player.container.statPoints--;
        strengthAdded++;
        uncommitedPoints++;
    }

    private void AddDexterity()
    {
        player.container.statPoints--;
        dexterityAdded++;
        uncommitedPoints++;
    }

    private void AddConstitution()
    {
        player.container.statPoints--;
        constitutionAdded++;
        uncommitedPoints++;
    }

    private void AddIntelligence()
    {
        player.container.statPoints--;
        intelligenceAdded++;
        uncommitedPoints++;
    }

    private void AddWisdom()
    {
        player.container.statPoints--;
        wisdomAdded++;
        uncommitedPoints++;
    }

    private void AddCharisma()
    {
        player.container.statPoints--;
        charismaAdded++;
        uncommitedPoints++;
    }

    private void RemoveStrength()
    {
        player.container.statPoints++;
        strengthAdded--;
        uncommitedPoints--;
    }

    private void RemoveDexterity()
    {
        player.container.statPoints++;
        dexterityAdded--;
        uncommitedPoints--;
    }

    private void RemoveConstitution()
    {
        player.container.statPoints++;
        constitutionAdded--;
        uncommitedPoints--;
    }

    private void RemoveIntelligence()
    {
        player.container.statPoints++;
        intelligenceAdded--;
        uncommitedPoints--;
    }

    private void RemoveWisdom()
    {
        player.container.statPoints++;
        wisdomAdded--;
        uncommitedPoints--;
    }

    private void RemoveCharisma()
    {
        player.container.statPoints++;
        charismaAdded--;
        uncommitedPoints--;
    }
}
