using System;
using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Interactors;
using Code.Services.Progress;
using Code.Services.SaveLoadService;
using CodeBase.Services.StaticDataService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        private ShapeSizeConfig _shapeSizeConfig;
        private GameObjectPool<Shape> _shapesPool;

        public ShapeFactory(IStaticDataService staticDataService,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IGlobalGameObjectPool globalGameObjectPool)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;

            _shapeSizeConfig = _staticDataService.ShapeSizeConfig;

            _shapesPool = new GameObjectPool<Shape>(_shapeSizeConfig.ShapePrefab, 20, globalGameObjectPool);
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
            float size = _shapeSizeConfig.Sizes[shapeIndex];
            Shape shape = _shapesPool.Get();
            shape.transform.position = at;
            shape.GetComponentInChildren<SpriteRenderer>().sprite = _shapeSizeConfig.Sprites[shapeIndex];

            InitializeShape(shapeSize, shape, shapeId, size);
            return shape;
        }

        private void InitializeShape(ShapeSize shapeSize, Shape shape, string newShapeId, float size)
        {
            shape.Construct(shapeSize, this, _progressService,
                () =>
                {
                    _saveLoadService.RemoveUpdatebleProgress(shape);
                    _shapesPool.Return(shape);
                },
                newShapeId);
            shape.transform.localScale = new Vector2(size, size);
            _saveLoadService.AddUpdatebleProgress(shape);
        }
    }
}