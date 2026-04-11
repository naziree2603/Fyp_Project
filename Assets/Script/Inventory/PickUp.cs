using UnityEngine;
using UnityEngine.Rendering;

public class PickUp : MonoBehaviour
{
    public Items item;
    private float pickupDelay = 1f;
    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    public bool CanPickUp()
    {
        return Time.time >= spawnTime + pickupDelay;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CanPickUp())
        {
            // add the item to the player's inventory
            InventoryManager.instance.AddItem(item);
            
            Destroy(gameObject);
        }
    }
}
