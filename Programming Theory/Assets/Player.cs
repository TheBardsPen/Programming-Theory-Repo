using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            inventory.Save();
        }
        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            inventory.Load();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }
}
