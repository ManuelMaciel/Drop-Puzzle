using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Repositories;
using Code.Services.Progress;
using CodeBase.Services.StaticDataService;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;

        LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService,
            IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            ProgressInitializer progressInitializer =
                new ProgressInitializer(_persistentProgressService.InteractorContainer, _staticDataService);
            
            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Gameplay.ToString());
        }

        public void Exit()
        {
        }
    }
}