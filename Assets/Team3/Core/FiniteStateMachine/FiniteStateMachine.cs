namespace Team3.StateMachine
{
    public class FiniteStateMachine
    {
        public State CurrentState { get; protected set; }
        public State LastState { get; private set; }

        public FiniteStateMachine(State firstState)
        {
            CurrentState = firstState;
        }

        public void ChangeState(State newState)
        {
            if (newState == CurrentState)
            { return; }
            
            CurrentState.Exit();
            LastState = CurrentState;
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}

