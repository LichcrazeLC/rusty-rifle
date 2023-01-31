using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Item
{
    public Bullet(BulletStats stats, InventoryManager manager, InventoryItemControllerUI controller) : base(stats, manager)
    {

        this.itemController = controller;
        IncreaseAmount(10);
    }

    public void IncreaseAmount(int nr)
    {
        amount += nr;
        
        this.itemController.UpdateBulletNumber(amount);
    }


    public override void DecreaseAmount()
    {
        base.DecreaseAmount();

        this.itemController.UpdateBulletNumber(amount);
    }
}
