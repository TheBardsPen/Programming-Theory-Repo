using UnityEngine;

[CreateAssetMenu(fileName = "New Accessory Object", menuName = "Inventory System/Items/Accessory")]
public class AccessoryObject : ItemObject
{
    public int healthBonus;
    public int manaBonus;
    public int strngthBonus;
    public int dexterityBonus;
    public int constitutionBonus;
    public int intelligenceBonus;
    public int wisdomBonus;
    public int charismaBonus;
    public AccessoryType accessoryType;

    public int value;

    private void Awake()
    {
        type = ItemType.Accessories;
    }
}
