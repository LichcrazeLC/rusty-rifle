using System.Collections;
using Agent_Behaviour;
using Agent_Behaviour.states;
using UnityEngine;

public class RunningToEnemyState : AgentState
{
    private Coroutine coroutine;

    public RunningToEnemyState(Agent agent, AgentStateMachine stateMachine) : base(agent, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // GameObject newEnemy = agent.ActiveEnemies.Dequeue();
        // agent.currentEnemy = newEnemy.GetComponent<Agent>();
        // agent.animator.SetBool(Running, true);
        // agent.MoveTo(newEnemy.transform.position);
        // agent.agent.stoppingDistance = 1f;
        // //todo: Fix sometimes throws exception
        // coroutine = agent.StartCoroutine(UpdateDestination());
        // ElfLogger.CombatLog("EnteredRunningToEnemy", agent.gameObject.tag);
    }

    public override void Exit()
    {
        base.Exit();
        agent.animator.SetBool(Running, false);
        if (coroutine != null)
            agent.StopCoroutine(coroutine);
        // ElfLogger.CombatLog("ExitedRunningToEnemy", agent.gameObject.tag);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        agent.RotateTowards(agent.agent.steeringTarget);
        // if (agent.currentEnemy)
        // {
        //     if (agent.IsInAttackRangeOf(agent.currentEnemy.transform))
        //     {
        //         agent.MoveTo(agent.currentEnemy.transform.position);
        //         stateMachine.ChangeState(agent.Fighting);
        //     }
        // }
        // else
        // {
        //     stateMachine.ChangeState(agent.Idle);
        // }
    }

    // private IEnumerator UpdateDestination()
    // {
        // while (true)
        // {
        //     yield return new WaitForSeconds(0.5f);
        //     if (agent.currentEnemy == null)
        //         break;
        //     agent.MoveTo(agent.currentEnemy.transform.position);
        // }
    // }
}