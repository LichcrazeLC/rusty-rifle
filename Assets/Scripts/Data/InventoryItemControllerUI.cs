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
    private RectTransform rectTransform;

    public void Initialize(Sprite gunIcon, Sprite bulletIcon, string weaponName, int bulletAmount)
    {
        this.gunIcon.sprite = gunIcon;
        this.bulletIcon.sprite = bulletIcon;
        gunName.SetText(weaponName);
        bulletNr.SetText(bulletAmount.ToString());

        rectTransform = GetComponent<RectTransform>();
    }

    public void UpdateBulletNumber(int bulletAmount)
    {
        bulletNr.SetText(bulletAmount.ToString());
    }

    public void Enlarge()
    {
        rectTransform.localScale = new Vector3(1.2f, 1.2f, 1f);
    }

    public void Normalize()
    {
        rectTransform.localScale = Vector3.one;
    }

}
