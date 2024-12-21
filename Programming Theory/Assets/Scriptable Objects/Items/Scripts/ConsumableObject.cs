using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Object", menuName = "Inventory System/Items/Consumable")]
public class ConsumableObject : ItemObject
{
    public int restoreHealthValue;

    public int value;

    private void Awake()
    {
        type = ItemType.Consumable;
    }
}
