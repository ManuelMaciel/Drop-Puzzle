using System;
using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Interactors;
using Code.Runtime.Logic.Gameplay;
using Code.Runtime.Services.Progress;
using Code.Runtime.Services.SaveLoadService;
using Code.Runtime.Services.StaticDataService;
using Plugin.DocuFlow.Documentation;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Factories
{
    [Doc("The ShapeFactory class is responsible for creating shape objects in the game. It manages the instantiation of shapes, including their size, appearance, and initialization. The ShapeFactory utilizes a pool of shape objects to optimize performance and manage object reuse.")]
    public class ShapeFactory : IShapeFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        private ShapeConfig _shapeConfig;
        private ShapePool _shapesPool;

        public ShapeFactory(IStaticDataService staticDataService,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IGameObjectsPoolContainer gameObjectsPoolContainer, DiContainer diContainer)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;

            _shapeConfig = _staticDataService.ShapeConfig;

            InitializeShapePool(progressService, saveLoadService, gameObjectsPoolContainer, diContainer);
        }

        public Shape CreateShape(Vector3 at, ShapeSize shapeSize, bool isDropped = false)
        {
            string shapeId = Guid.NewGuid().ToString();

            Shape shape = InstantiateShape(at, shapeSize, shapeId);

            if (isDropped)
                _progressService.InteractorContainer.Get<GameplayShapesInteractor>().AddShape(shape);

            return shape;
        }

        public Shape CreateShapeFromLoadedData(Vector3 at, ShapeSize shapeSize, string shapeId)
        {
            Shape shape = InstantiateShape(at, shapeSize, shapeId);

            return shape;
        }

        private Shape InstantiateShape(Vector3 at, ShapeSize shapeSize, string shapeId)
        {
            int shapeIndex = (int)shapeSize;
            float size = _shapeConfig.Sizes[shapeIndex];
            Shape shape = _shapesPool.Get(at);
            shape.transform.eulerAngles = Vector3.zero;
            shape.GetComponentInChildren<SpriteRenderer>().sprite = _shapeConfig.Sprites[shapeIndex];

            InitializeShape(shapeSize, shape, shapeId, size);
            return shape;
        }

        private void InitializeShape(ShapeSize shapeSize, Shape shape, string newShapeId, float size)
        {
            shape.Initialize(shapeSize, newShapeId);
            shape.transform.localScale = new Vector2(size, size);

            _saveLoadService.AddUpdatebleProgress(shape);
        }

        private void InitializeShapePool(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IGameObjectsPoolContainer gameObjectsPoolContainer, DiContainer diContainer)
        {
            _shapesPool = new ShapePool(_shapeConfig.shapePrefab, ObjectPoolStaticData.PreloadShapesCount,
                gameObjectsPoolContainer, diContainer,
                (shape) => shape.Construct(this, progressService, saveLoadService, _shapesPool));
            _shapesPool.Initialize();
        }
    }
}