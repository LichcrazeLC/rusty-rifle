using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Agent : MonoBehaviour
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public Animator animator;
    public NavMeshAgent agent;
    public Vector3 idleDestination;
    private bool _isActivated;
    private const float RotationSpeed = 10f;
    private Transform _mainBody;
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Speed = Animator.StringToHash("MotionMagnitude");
    private GameObject _damagePopupWrapper;
    public ParticleSystem gunFire;
    public GameObject gunImpact;
    public PlayerLogic playerRef;
    public Transform playerTransform;
    public Slider healthSlider;
    public Transform firePoint;
    public int damage;

    public void MoveTo(Vector3 goal)
    {
        if (agent.isOnNavMesh)
            agent.destination = goal;
    }

    public void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction = new Vector3(direction.x, 0, direction.z);
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        }
    }

    public void Shoot()
    {
        if (!IsActive()) return;
        animator.SetTrigger("Shoot");
        gunFire.Play();
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, transform.forward, out hit, 18f))
        {
            GameObject impact = Instantiate(gunImpact, hit.point, Quaternion.identity);
            if (hit.collider.CompareTag("Player"))
            {
                playerRef.TakeDamage(-damage);
            }
        }
    }

    public bool IsAtIdlePosition()
    {
        var distance = Vector3.Distance(transform.position, idleDestination);
        return distance < agent.stoppingDistance;
    }

    public void ApplyDestination(Vector3 destination)
    {
        idleDestination = destination;
    }

    public void KillAgent()
    {
        _isActivated = false;
        agent.enabled = false;
        animator.SetTrigger(Death);
        StartCoroutine(DestroyAgentDelayed());
    }

    public void UpdateCurrentHealth(float amount)
    {
        if (!IsActive())
        {
            return;
        }
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        if (CurrentHealth <= 0)
        {
            KillAgent();
        }

        healthSlider.value = CalculateHealth();
    }

    private IEnumerator DestroyAgentDelayed()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }

    private float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    private void FixedUpdate()
    {
        if (!IsActive()) return;

        MoveTo(playerTransform.position);
    }

    protected virtual void Update()
    {
        if (!IsActive()) return;

        animator.SetFloat(Speed, agent.velocity.magnitude);

        RotateTowards(playerTransform.position);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf && _isActivated;
    }

    public void DeactivateAgent()
    {
        _isActivated = false;
    }


    public void ActivateAgent()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

        MaxHealth = 100;
        CurrentHealth = MaxHealth;
        healthSlider.value = CalculateHealth();
        _isActivated = true;
        InvokeRepeating("Shoot", 2.0f, 2f);
    }
}