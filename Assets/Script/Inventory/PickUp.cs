using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Items item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // add the item to the player's inventory
            InventoryManager.instance.AddItem(item);
            //InventoryManager.instance.ShowItems();
            //InventoryManager.instance.Save();
            Destroy(gameObject);
        }
    }
}
