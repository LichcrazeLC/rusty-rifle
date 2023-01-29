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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Shoot()
    {
        animator.SetTrigger("Shoot");
        gunFire.Play();
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 20f))
        {
            Debug.Log(hit.collider.tag);
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
