using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun Stats")]
public class GunStats : ItemStats
{
   public float damage;
   public float range;
   public BulletStats bulletStats;
   public GameObject Model;
   public GameObject Impact;

}
