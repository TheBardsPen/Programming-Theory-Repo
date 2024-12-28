using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public int damage;
    public int defense;
    public int accuracy;
    public int strengthRequired;
    public int dexterityRequired;
    public ElementType elementType;
    public DamageType damageType;
    public WeaponType weaponType;

    public int value;

    private void Awake()
    {
        type = ItemType.Weapons;
    }
}
