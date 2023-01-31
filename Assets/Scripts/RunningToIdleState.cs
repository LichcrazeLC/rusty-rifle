

namespace Agent_Behaviour.states
{
    public class RunningToIdleState : AgentState
    {
        public RunningToIdleState(Agent agent, AgentStateMachine stateMachine) : base(agent, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            agent.animator.SetBool(Idle, false);
            agent.animator.SetBool(Running, true);
            agent.animator.Play("Running", -1, 0.05f);
            agent.MoveTo(agent.idleDestination);
            agent.agent.stoppingDistance = 0.2f;
        }

        public override void Exit()
        {
            base.Exit();
            agent.animator.SetBool(Running, false);
        }

        public override void LogicUpdate()
        {
            // base.LogicUpdate();
            // agent.RotateTowards(agent.agent.steeringTarget);
            // if (agent.agent.destination != agent.idleDestination)
            // {
            //     agent.MoveTo(agent.idleDestination);
            // }
            // if (agent.ActiveEnemies.Count != 0)
            // {
            //     stateMachine.ChangeState(agent.RunningToEnemy);
            // }
            // else if (agent.IsAtIdlePosition())
            // {
            //     agent.IdleDestinationReached();
            // }
        }
    }
}