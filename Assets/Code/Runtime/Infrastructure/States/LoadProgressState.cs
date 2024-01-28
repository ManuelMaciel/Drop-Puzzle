using Code.Runtime.Infrastructure.StateMachines;
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
            PlayerProgress playerProgress = new PlayerProgress();
            
            playerProgress.MoneyRepository.Coins = 999;
            
            InteractorsInitializer.Initialize(playerProgress, _persistentProgressService.InteractorContainer,
                _staticDataService);

            InitializeSaveLoadService(playerProgress);
            // playerProgress = _saveLoadService.LoadProgress();

            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Gameplay.ToString());
        }

        public void Exit()
        {
        }

        private void InitializeSaveLoadService(PlayerProgress playerProgress)
        {
            _saveLoadService.Initialize(playerProgress);
        }
    }
}