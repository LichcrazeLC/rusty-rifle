using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType { RifleRound, ArcaneCore, Stone }


[CreateAssetMenu(menuName = "Bullet Stats")]
public class BulletStats : ItemStats
{
    public BulletType bulletType;
}
