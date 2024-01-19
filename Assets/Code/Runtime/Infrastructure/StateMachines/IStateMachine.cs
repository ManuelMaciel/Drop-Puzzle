using Code.Runtime.Infrastructure.States;

namespace Code.Runtime.Infrastructure.StateMachines
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        void RegisterState<TState>(TState state) where TState : IExitableState;
    }
}