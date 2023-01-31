using System.Collections.Generic;
using System.Linq;
using Agent_Behaviour;
using Agent_Behaviour.states;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro;

public class Agent : MonoBehaviour
{
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public Animator animator;
    public NavMeshAgent agent;
    public Vector3 idleDestination;
    // states
    public AgentState Idle;
    public RunningToIdleState RunningToIdle;
    public RunningToEnemyState RunningToEnemy;
    public FightingState Fighting;
    protected AgentStateMachine StateMachine;
    private bool _isActivated;
    private const float RotationSpeed = 10f;
    // private HealthController healthController;
    private Transform _mainBody;
    private Transform _healthBarPosMarker;
    private static readonly int Death = Animator.StringToHash("Death");
    private GameObject _damagePopupWrapper;
    private GameObject _auraPopupWrapper;
    private GameObject _heroHealthBarWrapper;
    private GameObject _enemyHealthBarWrapper;

    public ParticleSystem gunFire;

    public Shooting playerRef;



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


    public void ReceiveDamage(float damage, bool critical)
    {
        if (damage < 0)
            damage = 0;
        UpdateCurrentHealth(-damage);
    }

    private void SpawnDamagePopup(int damage, bool critical)
    {
        // Canvas mainCanvas = UnityUtils.GetMainCanvas();
        // Vector3 preScaledPosition = Camera.main.WorldToScreenPoint(_mainBody.transform.position);
        // float scaleFactor = mainCanvas.scaleFactor;
        // Vector2 finalPosition = new Vector2(preScaledPosition.x / scaleFactor,
        //     preScaledPosition.y / scaleFactor + 50f);

        // GameObject damagePopup = Instantiate(ResourcesLoader.LoadDamagePopup(), finalPosition, Quaternion.identity);
        // var mesh = damagePopup.GetComponentInChildren<TextMeshProUGUI>();
        // mesh.text = "" + damage;
        // mesh.color = critical ? Color.red : mesh.color;
        // mesh.fontSize = critical ? 140f : 86f;
        // damagePopup.transform.SetParent(_damagePopupWrapper.transform, false);
    }

    public void Shoot()
    {
        animator.SetTrigger("Shoot");

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

    public virtual void Activate()
    {
        ActivateAgent();
    }

    public void KillAgent()
    {
        // _isActivated = false;
        // agent.enabled = false;
        // if (healthController != null)
        //     healthController.gameObject.SetActive(false);
        // animator.SetBool(Death, true);
        // if (animator.GetBool(Death))
        //     StartCoroutine(DestroyAgentDelayed());
        // else
        //     DestroyAgent();
    }

    public void UpdateCurrentHealth(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        // healthController.SetHealth(CalculateHealth());
    }

    private IEnumerator DestroyAgentDelayed()
    {
        yield return new WaitForSeconds(4);
        // DestroyAgent();
    }

    private float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    private void FixedUpdate()
    {
        if (!IsActive()) return;
        StateMachine.currentState.PhysicsUpdate();
    }


    protected virtual void Update()
    {
        if (!IsActive()) return;
        StateMachine.currentState.HandleInput();
        StateMachine.currentState.LogicUpdate();
        // animator.SetFloat(Speed, agent.velocity.magnitude);
    }

    // private HealthController GetHealthBar()
    // {
    //     GameObject healthBar = Tag.ENEMY.Compare(gameObject)
    //         ? ResourcesLoader.LoadEnemyHealthBar()
    //         : ResourcesLoader.LoadHeroHealthBar();

    //     GameObject healthBarWrapper =
    //         UnityUtils.InstantiateUIElementAtWorldPosition(healthBar);

    //     HealthController healthController = healthBarWrapper.GetComponent<HealthController>();
    //     if (Tag.ENEMY.Compare(gameObject))
    //     {
    //         healthBarWrapper.transform.SetParent(_enemyHealthBarWrapper.transform);
    //     }
    //     else
    //     {
    //         healthBarWrapper.transform.SetParent(_heroHealthBarWrapper.transform);
    //     }

    //     healthController.Initialize(_healthBarPosMarker, this.gameObject, new Vector2(0, 0f));

    //     return healthController;
    // }

    public bool IsActive()
    {
        return gameObject.activeSelf && _isActivated;
    }

    private void ActivateAgent()
    {
        // Canvas mainCanvas = UnityUtils.GetMainCanvas();

        _mainBody = transform.Find("Body");
        _healthBarPosMarker = transform.Find("HealthBarPosMarker");

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;


        CurrentHealth = MaxHealth;

        // healthController = GetHealthBar();
        // healthController.SetHealth(CalculateHealth());

        StateMachine = new AgentStateMachine();
        _isActivated = true;
    }
}