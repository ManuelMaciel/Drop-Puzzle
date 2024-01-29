using Code.Runtime.Extensions;
using Code.Runtime.Interactors;
using Code.Runtime.Logic;
using Code.Runtime.UI;
using Code.Services.Progress;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly HUD.Factory _hudFactory;
        private readonly Spawner.Factory _spawnerFactory;
        private readonly IShapeFactory _shapeFactory;
        private readonly IPersistentProgressService _progressService;

        LoadLevelState(ISceneLoader sceneLoader, HUD.Factory hudFactory, Spawner.Factory spawnerFactory,
            IShapeFactory shapeFactory, IPersistentProgressService progressService)
        {
            _sceneLoader = sceneLoader;
            _hudFactory = hudFactory;
            _spawnerFactory = spawnerFactory;
            _shapeFactory = shapeFactory;
            _progressService = progressService;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, InitWorld);
        }

        private void InitWorld()
        {
            _hudFactory.Create(InfrastructureAssetPath.HUDPath);
            _spawnerFactory.Create(InfrastructureAssetPath.ShapeDropper);

            SpawnLoadedShapes();
        }

        private void SpawnLoadedShapes()
        {
            var gameplayShapesInteractor = _progressService.InteractorContainer.Get<GameplayShapesInteractor>();

            foreach (var shapeData in gameplayShapesInteractor.GetShapesData())
            {
                _shapeFactory.CreateShape(shapeData.Position.AsUnityVector(), shapeData.ShapeSize, true);
                GameObject gameObject = new GameObject(shapeData.ShapeSize.ToString());
                gameObject.transform.position = shapeData.Position.AsUnityVector();
            }
        }

        public void Exit()
        {
        }
    }
}