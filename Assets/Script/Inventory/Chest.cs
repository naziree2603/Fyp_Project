using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform itemPlace;
    [SerializeField] private int minItem = 2;
    [SerializeField] private int maxItem = 3;
    private bool isOpen = false;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    private void OpenChest()
    {
        if (isOpen) return;
        isOpen = true;

        List<Items> items = InventoryManager.instance.GetRandomItems(minItem, maxItem);
        if (items.Count == 0) return;
        int i = 0;
        foreach (Items item in items)
        {
            Vector3 offset = new Vector3(i * 0.5f, 0, 0);
            Vector3 randomPlace = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(0, 1));
            GameObject GO = Instantiate(item.groundedPrefab, itemPlace.position + randomPlace + offset, Quaternion.identity);
            Rigidbody rb = GO.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.freezeRotation = true;

            Vector3 shootDir = Vector3.up * 0.5f;
            rb.AddForce(shootDir * 4f, ForceMode.Impulse);
            i++;
        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("Open");
            OpenChest();
        }
    }
}
