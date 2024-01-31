using Code.Runtime.Extensions;
using Code.Runtime.Interactors;
using Code.Runtime.Logic;
using Code.Runtime.UI;
using Code.Services.Progress;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly HUD.Factory _hudFactory;
        private readonly Spawner.Factory _spawnerFactory;
        private readonly IShapeFactory _shapeFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _progressService;

        LoadLevelState(HUD.Factory hudFactory, Spawner.Factory spawnerFactory,
            IShapeFactory shapeFactory, IUIFactory uiFactory, IPersistentProgressService progressService)
        {
            _hudFactory = hudFactory;
            _spawnerFactory = spawnerFactory;
            _shapeFactory = shapeFactory;
            _uiFactory = uiFactory;
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
            _uiFactory.CreateUIRoot();

            SpawnLoadedShapes();
        }

        private void SpawnLoadedShapes()
        {
            var gameplayShapesInteractor = _progressService.InteractorContainer.Get<GameplayShapesInteractor>();

            foreach (var shapeData in gameplayShapesInteractor.GetShapesData())
            {
                _shapeFactory.CreateShape(shapeData.Position.AsUnityVector(), shapeData.ShapeSize, true);
            }
        }

        public void Exit()
        {
        }
    }
}