using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    public Gun(GunStats stats, InventoryManager manager) : base(stats, manager)
    {   
        GameObject itemUI = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Item"));
        this.itemController = itemUI.GetComponent<InventoryItemControllerUI>();
        itemUI.transform.SetParent(manager.inventoryUIPanel, false);

        IncreaseAmount();
        itemController.Initialize(stats.icon, stats.bulletStats.icon, stats.id, amount);
        
        manager.Add(stats.bulletStats, this.itemController);

    }
    public override void IncreaseAmount()
    {
        if (amount < 1)
            amount++;

    }

}
