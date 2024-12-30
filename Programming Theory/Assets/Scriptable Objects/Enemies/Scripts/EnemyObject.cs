using Ink.Parsed;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy")]
public class EnemyObject : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public enum ElementDefense
    {
        None,
        Fire,
        Water,
        Air,
        Earth,
        Lightning,
        Poison,
        Light,
        Dark
    }
    public int defense;
    public int damage;
    public float health;
    public float maxHealth;
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;
    public int goldDropped;
    public int expDropped;
    [Tooltip("Lower is better. Determines seconds to wait between attacks.")]
    public float agility;
    public List<ItemObject> droppableItems = new List<ItemObject>();
    public List<float> dropChance = new List<float>();
}
