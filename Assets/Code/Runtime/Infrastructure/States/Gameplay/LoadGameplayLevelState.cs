using Code.Runtime.Extensions;
using Code.Runtime.Interactors;
using Code.Runtime.Logic;
using Code.Runtime.UI;
using Code.Services.Progress;

namespace Code.Runtime.Infrastructure.States.Gameplay
{
    public class LoadGameplayLevelState : IState
    {
        private readonly PrefabFactory<HUD> _hudFactory;
        private readonly PrefabFactory<Spawner> _spawnerFactory;
        private readonly IShapeFactory _shapeFactory;
        private readonly IPersistentProgressService _progressService;

        LoadGameplayLevelState(PrefabFactory<HUD> hudFactory, PrefabFactory<Spawner> spawnerFactory,
            IShapeFactory shapeFactory, IUIFactory uiFactory, IPersistentProgressService progressService)
        {
            _hudFactory = hudFactory;
            _spawnerFactory = spawnerFactory;
            _shapeFactory = shapeFactory;
            _progressService = progressService;
        }

        public void Enter()
        {
            InitWorld();
        }

        private void InitWorld()
        {
            _hudFactory.Create(InfrastructureAssetPath.HUDPath);
            _spawnerFactory.Create(InfrastructureAssetPath.ShapeDropperPath);

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