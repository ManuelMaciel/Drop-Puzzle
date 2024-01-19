using Code.Runtime.Infrastructure.States;

namespace Code.Runtime.Infrastructure
{
    public interface IStatesFactory
    {
        TState Create<TState>() where TState : IExitableState;
    }
}