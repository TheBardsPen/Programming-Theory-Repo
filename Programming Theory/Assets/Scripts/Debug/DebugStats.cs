using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;

public class DebugStats : MonoBehaviour
{
    public PlayerObject player;
    public TMP_Dropdown statSelect;
    public TMP_InputField amountInput;

    private List<Action> statChanges = new List<Action>();
    private int requiredExpForLevel;

    private void Start()
    {
        statChanges.Add(LevelUp);
        statChanges.Add(AddExp);
        statChanges.Add(AddStrength);
        statChanges.Add(AddDexterity);
        statChanges.Add(AddConstitution);
        statChanges.Add(AddIntelligence);
        statChanges.Add(AddWisdom);
        statChanges.Add(AddCharisma);
    }
    private void OnEnable()
    {
        print(requiredExpForLevel + " exp is required for level up.");
    }

    private void Update()
    {
        requiredExpForLevel = (((player.container.level * 9) * (player.container.level * 7)) - player.container.exp);

        if (requiredExpForLevel <= 0)
        {
            LevelUp();
        }
    }

    public void ChangeStat()
    {
        statChanges[statSelect.value]();

        //if (statSelect.value.ToString() == )
    }

    public void LevelUp()
    {
        print("you leveld up");
        player.container.level++;
        player.container.maxHealth += Mathf.Round(player.container.constitution * 1.5f);
        player.container.health = player.container.maxHealth;
        player.container.maxMana += Mathf.Round(player.container.wisdom / 2);
        player.container.mana = player.container.maxMana;
        player.container.statPoints++;
        player.container.exp = 0;
    }

    private void AddExp()
    {
        print("you added exp");
        player.container.exp += int.Parse(amountInput.text);
    }

    private void AddStrength()
    {
        print("you added strength");
        player.container.strength += int.Parse(amountInput.text);
    }

    private void AddDexterity()
    {
        print("you added dexterity");
        player.container.dexterity += int.Parse(amountInput.text);
    }

    private void AddConstitution()
    {
        print("you added constitution");
        player.container.constitution += int.Parse(amountInput.text);
        player.container.maxHealth += int.Parse(amountInput.text) * 5;
    }

    private void AddIntelligence()
    {
        print("you added intelligence");
        player.container.intelligence += int.Parse(amountInput.text);
    }

    private void AddWisdom()
    {
        print("you added wisdom");
        player.container.wisdom += int.Parse(amountInput.text);
        player.container.mana += int.Parse(amountInput.text) * 2;
    }

    private void AddCharisma()
    {
        print("you added charisma");
        player.container.charisma += int.Parse(amountInput.text);
    }
}
