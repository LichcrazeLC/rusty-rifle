using Agent_Behaviour.states;

namespace Agent_Behaviour
{
    public class AgentStateMachine
    {
        public AgentState currentState;

        public void Initialize(AgentState startingState)
        {
            currentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(AgentState newState)
        {
            currentState.Exit();

            currentState = newState;
            newState.Enter();
        }
    }
}