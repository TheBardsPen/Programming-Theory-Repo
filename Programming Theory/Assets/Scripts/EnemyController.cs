using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyObject enemy;

    private bool isCooldownActive;
    private TextMeshProUGUI battlelog;

    private void Start()
    {
        battlelog = GameUIHandler.instance.battleText;
    }

    private void Update()
    {
        if (!isCooldownActive)
        {
            StartCoroutine(Attack());
        }
        if (enemy.health <= 0)
        {
            DataManager.instance.player.container.exp += enemy.expDropped;
            DataManager.instance.player.container.gold += enemy.goldDropped;
            float dropChance = Random.Range(0f, 1f);
            int droppedItem = Random.Range(0, enemy.droppableItems.Count);
            if (enemy.dropChance[droppedItem] <= dropChance)
            {
                DataManager.instance.inventory.AddItem(enemy.droppableItems[droppedItem], 1);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator Attack()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(enemy.agility);
        int damageDealt = enemy.damage;
        DataManager.instance.player.container.health -= damageDealt;
        battlelog.text += $"\n{enemy.enemyName} did {damageDealt} damage!";
        isCooldownActive = false;
    }
}
