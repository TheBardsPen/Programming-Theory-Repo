using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor")]
public class ArmorObject : ItemObject
{
    public int defense;
    public int strengthRequired;
    public int dexterityRequired;
    public ElementType elementType;
    public ArmorType armorType;

    public int value;

    private void Awake()
    {
        type = ItemType.Armor;
    }
}
