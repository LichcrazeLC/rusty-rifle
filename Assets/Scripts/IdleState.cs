using Agent_Behaviour;
using Agent_Behaviour.states;

public class IdleState : AgentState
{
    public IdleState(Agent agent, AgentStateMachine stateMachine) : base(agent, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        agent.animator.SetBool(Idle, true);
    }

    public override void Exit()
    {
        base.Exit();
        agent.animator.SetBool(Idle, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // if (agent.ActiveEnemies.Count != 0)
        // {
        //     stateMachine.ChangeState(agent.RunningToEnemy);
        // }
        // else if (!agent.IsAtIdlePosition())
        // {
        //     stateMachine.ChangeState(agent.RunningToIdle);
        // }
    }
}