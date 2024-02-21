using Code.Runtime.Configs;
using Code.Runtime.Extensions;
using Code.Runtime.Interactors;
using Code.Runtime.Logic.Factories;
using Code.Runtime.Logic.Gameplay;
using Code.Runtime.Services.Progress;
using Code.Runtime.Services.StaticDataService;
using Code.Runtime.UI;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class LoadGameplayLevelState : IState
    {
        private readonly PrefabFactory<HUD> _hudFactory;
        private readonly PrefabFactory<Spawner> _spawnerFactory;
        private readonly IShapeFactory _shapeFactory;
        private readonly IPersistentProgressService _progressService;
        private GameplayAssetsConfig _gameplayAssetsConfig;

        LoadGameplayLevelState(PrefabFactory<HUD> hudFactory, PrefabFactory<Spawner> spawnerFactory,
            IShapeFactory shapeFactory, IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            _hudFactory = hudFactory;
            _spawnerFactory = spawnerFactory;
            _shapeFactory = shapeFactory;
            _progressService = progressService;
            _gameplayAssetsConfig = staticDataService.GameplayAssetsConfig;
        }

        public void Enter()
        {
            InitWorld();
        }

        private void InitWorld()
        {
            _hudFactory.Create(_gameplayAssetsConfig.HUD);
            _spawnerFactory.Create(_gameplayAssetsConfig.ShapeDropper);
            _spawnerFactory.InstantiatedPrefab.Initialize(_gameplayAssetsConfig.SpawnPointPosition);

            SpawnLoadedShapes();
        }

        private void SpawnLoadedShapes()
        {
            var gameplayShapesInteractor = _progressService.InteractorContainer.Get<GameplayShapesInteractor>();

            foreach (var shapeData in gameplayShapesInteractor.GetShapesData())
            {
                _shapeFactory.CreateShapeFromLoadedData(shapeData.Position.AsUnityVector(), shapeData.ShapeSize, shapeData.Id);
            }
        }

        public void Exit()
        {
        }
    }
}