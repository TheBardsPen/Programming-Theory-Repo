using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {

    }
}
