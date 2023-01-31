using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemControllerUI : MonoBehaviour
{

    [SerializeField]
    private Image gunIcon;
    [SerializeField]
    private Image bulletIcon;
    [SerializeField]
    private TextMeshProUGUI gunName;
    [SerializeField]
    private TextMeshProUGUI bulletNr;
    
    public void Initalize(Sprite gunIcon, Sprite bulletIcon, string weaponName, int bulletAmount)
    {
        this.gunIcon.sprite = gunIcon;
        this.bulletIcon.sprite = bulletIcon;
        gunName.SetText(weaponName);
        bulletNr.SetText(bulletAmount.ToString());
    }

    public void UpdateBulletNumber(int bulletAmount)
    {
        bulletNr.SetText(bulletAmount.ToString());
    }

}
