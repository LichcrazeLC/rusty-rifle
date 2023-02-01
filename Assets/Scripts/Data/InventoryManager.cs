using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<ItemStats, Item> itemDictionary;
    public List<Item> stash { get; set; }
    public Transform inventoryUIPanel;
    public List<ItemStats> gunReferences;
    private int currentlyEquippedWeaponIndex;

    //REF to player
    public PlayerLogic player;


    public void Awake()
    {
        stash = new List<Item>();
        itemDictionary = new Dictionary<ItemStats, Item>();

        //ADD STARTER WEAPON TO INVENTORY
        Add(gunReferences[0]);
        Add(gunReferences[1]);

    }

    public void Add(ItemStats stats, InventoryItemControllerUI controller = null)
    {
        if (itemDictionary.TryGetValue(stats, out Item item))
        {
            item.IncreaseAmount();
        }
        else
        {
            Item newItem;
            if (controller == null)
            {
                newItem = new Gun(stats as GunStats, this);
            }
            else
            {
                newItem = new Bullet(stats as BulletStats, this, controller);
            }

            stash.Add(newItem);
            itemDictionary.Add(stats, newItem);
        }
    }

    public bool Remove(ItemStats stats)
    {
        if (itemDictionary.TryGetValue(stats, out Item item))
        {
            if (item.amount > 0)
            {
                item.DecreaseAmount();
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public Item GetItem(ItemStats stats)
    {
        return itemDictionary[stats];
    }

    public void ChangeWeapon()
    {
        currentlyEquippedWeaponIndex++;
        if (currentlyEquippedWeaponIndex > gunReferences.Count - 1)
        {
            currentlyEquippedWeaponIndex = 0;
        }
        GunStats gun = gunReferences[currentlyEquippedWeaponIndex] as GunStats;
        if (gun == player.equippedWeapon)
        {
            return;
        }

        itemDictionary[player.equippedWeapon].itemController.Normalize();

        if (itemDictionary.TryGetValue(gun, out Item item))
        {
            player.Equip(gun);
            item.itemController.Enlarge();
        }
    }

}
