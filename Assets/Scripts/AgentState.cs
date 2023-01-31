using UnityEngine;

namespace Agent_Behaviour.states
{
    public abstract class AgentState
    {
        protected static readonly int Attack = Animator.StringToHash("Attack");
        protected static readonly int IsInCombat = Animator.StringToHash("isInCombat");
        protected static readonly int Running = Animator.StringToHash("Running");
        protected static readonly int Idle = Animator.StringToHash("Idle");
        protected readonly Agent agent;
        protected readonly AgentStateMachine stateMachine;

        protected AgentState(Agent agent, AgentStateMachine stateMachine)
        {
            this.agent = agent;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void HandleInput()
        {
        }

        public virtual void LogicUpdate()
        {
            if (agent.CurrentHealth <= 0)
            {
                agent.KillAgent();
            }
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Exit()
        {
        }
    }
}