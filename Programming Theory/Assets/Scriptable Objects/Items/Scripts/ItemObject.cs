using UnityEngine;

public enum ItemType
{
    Consumables,
    Weapons,
    Armor,
    Accessories,
    KeyItems,
    Default
}

public enum ElementType
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

public enum DamageType
{
    None,
    Blunt,
    Pierce,
    Slash
}
public enum WeaponType
{
    Fist,
    Dagger,
    Sword,
    Longsword,
    Greatsword,
    Shield
}

public enum ArmorType
{
    Helmet,
    Chest,
    Arms,
    Hands,
    Legs,
    Feet
}

public enum AccessoryType
{
    Ring,
    Necklace
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
}
