using Code.Runtime.Infrastructure.States;
using Zenject;

namespace Code.Runtime.Infrastructure
{
    public class StatesFactory : IStatesFactory
    {
        private IInstantiator instantiator;

        public StatesFactory(IInstantiator instantiator)
            => this.instantiator = instantiator;

        public TState Create<TState>() where TState : IExitableState =>
            instantiator.Instantiate<TState>();
    }
}