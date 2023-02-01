using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int amount { get; set; }

    public ItemStats itemStats;
    public InventoryItemControllerUI itemController;

    public Item(ItemStats stats, InventoryManager manager)
    {
        itemStats = stats;
    }

    public virtual void IncreaseAmount()
    {
        amount++;
    }
    
    public virtual void DecreaseAmount()
    {
        if (amount > 0)
            amount--;
    }
}
