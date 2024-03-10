using Code.Runtime.Infrastructure.States;
using Plugin.DocuFlow.Documentation;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.Bootstrappers
{
    [Doc("The GameBootstrapper class is responsible for initializing the game state machine and managing the bootstrapping process (Start Game State Machine).")]
    public class GameBootstrapper : BootstrapperBase
    {
        private void Awake()
        {
             InitializeGameStateMachine();
             
             Application.targetFrameRate = 60;
             
             DontDestroyOnLoad(this.gameObject);
        }

        private void InitializeGameStateMachine()
        {
            _gameStateMachine.RegisterState(_statesFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<LoadProgressState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<LoadSceneState>());

            _gameStateMachine.Enter<BootstrapState>();
        }

        public class Factory : PlaceholderFactory<GameBootstrapper>
        {
        }
    }
}