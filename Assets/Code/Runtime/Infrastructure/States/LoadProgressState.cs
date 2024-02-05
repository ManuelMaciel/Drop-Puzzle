using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Runtime.Repositories;
using Code.Services.Progress;
using Code.Services.SaveLoadService;
using CodeBase.Services.StaticDataService;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticDataService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ISceneLoader _sceneLoader;

        LoadProgressState(IPersistentProgressService persistentProgressService,
            IStaticDataService staticDataService, ISaveLoadService saveLoadService, ISceneLoader sceneLoader)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            PlayerProgress playerProgress = InitializePlayerProgress();

            InteractorsInitializer.Initialize(playerProgress, _persistentProgressService.InteractorContainer,
                _staticDataService);

            _sceneLoader.Load(SceneName.Menu.ToString());
        }

        private PlayerProgress InitializePlayerProgress()
        {
            if (!_saveLoadService.TryLoadProgress(out PlayerProgress playerProgress))
            {
                playerProgress = new PlayerProgress();

                FirstLoadGameData(playerProgress);
            }

            _saveLoadService.Initialize(playerProgress);
            return playerProgress;
        }

        private void FirstLoadGameData(PlayerProgress playerProgress)
        {
            playerProgress.MoneyRepository.Coins = 999;
            playerProgress.PurchasesRepository.PurchasedBackgrounds.Add(BackgroundType.Default);
            playerProgress.PurchasesRepository.SelectedBackground = BackgroundType.Default;
        }

        public void Exit()
        {
        }
    }
}