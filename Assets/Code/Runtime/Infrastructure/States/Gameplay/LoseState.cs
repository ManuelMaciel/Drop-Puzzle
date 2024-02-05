using Code.Runtime.Interactors;
using Code.Runtime.Logic;
using Code.Runtime.UI;
using Code.Services.Progress;

namespace Code.Runtime.Infrastructure.States
{
    public class LoseState : IState
    {
        private readonly PrefabFactory<Spawner> _spawnerFactory;
        private readonly PrefabFactory<HUD> _hudFactory;
        private readonly IPersistentProgressService _progressService;

        public LoseState(PrefabFactory<Spawner> spawnerFactory, PrefabFactory<HUD> hudFactory,
            IPersistentProgressService progressService)
        {
            _spawnerFactory = spawnerFactory;
            _hudFactory = hudFactory;
            _progressService = progressService;
        }

        public void Enter()
        {
            _spawnerFactory.InstantiatedPrefab.gameObject.SetActive(false);

            _progressService.InteractorContainer.Get<GameplayShapesInteractor>().ClearShapesData();
            _progressService.InteractorContainer.Get<ScoreInteractor>().ResetCurrentScore();
            
            _hudFactory.InstantiatedPrefab.ChangeToLose();
        }

        public void Exit()
        {
        }
    }
}