using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLogic : MonoBehaviour
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

    public GunStats equippedWeapon;

    [SerializeField]
    private BulletStats currentBullet;

    [SerializeField]
    private GameObject physicalGun;
    public bool isActive;

    [SerializeField]
    private TextMeshProUGUI healthAmountText;

    [SerializeField]
    private GameManager gameManager;

    private Movement movController;

    void Start()
    {
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
        healthAmountText.text = CurrentHealth.ToString();
        animator = GetComponent<Animator>();
        movController = GetComponent<Movement>();

        Equip(inventory.gunReferences[0] as GunStats);
        inventory.ChangeWeapon();
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

        animator.SetTrigger("Equip");
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
            GameObject impact = Instantiate(equippedWeapon.Impact, hit.point, Quaternion.identity);
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<Agent>().UpdateCurrentHealth(-equippedWeapon.damage);

            }
        }

    }

    public void TakeDamage(float damage)
    {
        if (!isActive) return;

        CurrentHealth += damage;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        if (CurrentHealth <= 0)
        {
            animator.SetTrigger("Death");
            gameManager.LoseGame();
        }

        healthAmountText.text = CurrentHealth.ToString();
    }

    public void Deactivate()
    {

        movController.FreezeMovement();
        animator.Play("Motion Tree");
        isActive = false;
    }

    void Update()
    {

        if (!isActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            inventory.ChangeWeapon();
        }

        // TakeDamage(-.1f);
    }
}
