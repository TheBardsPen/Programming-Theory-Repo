using UnityEngine;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public PlayerObject player;

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI levelAndClass;
    public TextMeshProUGUI stats;

    public GameObject levelUpPanel;

    // Local variables needed to update display
    private int expToLevelUp;

    private void OnEnable()
    {
        playerName.text = player.container.name;
    }

    private void Update()
    {
        // Set exp needed to next level up
        expToLevelUp = (((player.container.level * 9) * (player.container.level * 7)) - player.container.exp);

        // Check if player has unused stat points
        if (player.container.statPoints > 0)
        {
            levelUpPanel.SetActive(true);
        }

        // Update class and level from player data
        levelAndClass.text = $"Lv. {player.container.level} {player.container.playerClass}";

        // Update remaining stats if level up panel is inactive
        if (!levelUpPanel.activeSelf)
        {
            stats.text =
            $"{player.container.strength}\r\n" +
            $"{player.container.dexterity}\r\n" +
            $"{player.container.constitution}\r\n" +
            $"{player.container.intelligence}\r\n" +
            $"{player.container.wisdom}\r\n" +
            $"{player.container.charisma}\r\n\r\n" +
            $"{expToLevelUp}";
        }
    }

    private void OnDisable()
    {
        // Disable level up panel if navigating away to prevent stat loss
        levelUpPanel.SetActive(false);
    }
}
