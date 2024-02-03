using Code.Runtime.Infrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.Bootstrappers
{
    public abstract class BootstrapperBase : MonoBehaviour
    {
        [Inject] protected GameStateMachine _gameStateMachine;
        [Inject] protected IStatesFactory _statesFactory;
    }
}