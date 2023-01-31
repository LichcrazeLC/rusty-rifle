using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform firePoint;
    private Animator animator;

    [SerializeField]
    private ParticleSystem gunFire;

    [SerializeField]
    private InventoryManager inventory;

    [SerializeField]
    private Transform gunHolder;

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }

    [SerializeField]
    private GunStats equippedWeapon;

    [SerializeField]
    private BulletStats currentBullet;

    [SerializeField]
    private GameObject physicalGun;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Equip(GunStats weapon)
    {

        if (physicalGun != null)
        {
            Destroy(physicalGun);
        }

        equippedWeapon = weapon;
        currentBullet = weapon.bulletStats;
        physicalGun = Instantiate(equippedWeapon.Model);
        physicalGun.transform.SetParent(gunHolder);
        physicalGun.transform.localPosition = Vector3.zero;
        physicalGun.transform.localRotation = Quaternion.identity;
        physicalGun.transform.localScale = Vector3.one;
        gunFire = physicalGun.GetComponentInChildren<ParticleSystem>();
    }

    void Shoot()
    {

        if (!inventory.Remove(currentBullet))
        {
            Debug.Log("No Bullets for this weapon");
            return;
        }

        animator.SetTrigger("Shoot");
        gunFire.Play();
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, equippedWeapon.range))
        {
            Debug.Log(hit.collider.tag);
        }

    }

    public void TakeDamage(float damage)
    {
        CurrentHealth += damage;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
}
