using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item Object", menuName = "Inventory System/Items/Key Item")]
public class KeyItemObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.KeyItems;
    }
}
