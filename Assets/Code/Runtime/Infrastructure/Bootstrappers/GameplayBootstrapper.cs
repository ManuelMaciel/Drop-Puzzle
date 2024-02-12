using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Gameplay;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.Bootstrappers
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        [Inject] private GameplayStateMachine _gameplayStateMachine;
        [Inject] private IStatesFactory _statesFactory;
        [Inject] private ISceneLoader _sceneLoader;

        private void Awake()
        {
            if (_sceneLoader.IsNameLoadedScene(SceneName.Gameplay.ToString()))
                AddGameplayStates();
        }

        private void AddGameplayStates()
        {
            _gameplayStateMachine.RegisterState(_statesFactory.Create<LoadGameplayLevelState>());
            _gameplayStateMachine.RegisterState(_statesFactory.Create<LoseState>());

            _gameplayStateMachine.Enter<LoadGameplayLevelState>();
        }
    }
}