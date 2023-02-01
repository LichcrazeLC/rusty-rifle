using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public List<ItemStats> stats;
    public InventoryManager inventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enered Collider");
            foreach (ItemStats model in stats)
            {
                inventory.Add(model);
            }

            Destroy(this.gameObject);
        }
    }
}
