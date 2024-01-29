using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using Code.Services.Progress;
using Code.Services.SaveLoadService;
using CodeBase.Services.StaticDataService;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly ISaveLoadService _saveLoadService;

        LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService,
            IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            PlayerProgress playerProgress = InitializePlayerProgress();

            InteractorsInitializer.Initialize(playerProgress, _persistentProgressService.InteractorContainer,
                _staticDataService);
            
            _saveLoadService.AddUpdatebleProgress(_persistentProgressService.InteractorContainer
                .Get<GameplayShapesInteractor>());

            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Gameplay.ToString());
        }

        private PlayerProgress InitializePlayerProgress()
        {
            if (!_saveLoadService.TryLoadProgress(out PlayerProgress playerProgress))
            {
                playerProgress = new PlayerProgress();
            }

            _saveLoadService.Initialize(playerProgress);
            return playerProgress;
        }

        public void Exit()
        {
        }
    }
}