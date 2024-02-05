using System;
using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Services.Progress;
using Code.Services.SaveLoadService;
using CodeBase.Services.StaticDataService;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Runtime.Logic
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private ShapeSizeConfig _shapeSizeConfig;
        private Shape _shapePrefab;

        public ShapeFactory(IStaticDataService staticDataService,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Initialize()
        {
            _shapeSizeConfig = _staticDataService.ShapeSizeConfig;
            _shapePrefab = _shapeSizeConfig.ShapePrefab;
        }
        
        public Shape CreateShape(Vector3 at, ShapeSize shapeSize, bool isDropped = false)
        {
            string shapeId = Guid.NewGuid().ToString();
            
            Shape shape = InstantiateShape(at, shapeSize, shapeId);

            if(isDropped) 
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
            int shapeIndex = (int) shapeSize;
            float size = _shapeSizeConfig.Sizes[shapeIndex];
            Shape shape = Object.Instantiate(_shapePrefab, at, Quaternion.identity);
            shape.GetComponent<SpriteRenderer>().sprite = _shapeSizeConfig.Sprites[shapeIndex];

            InitializeShape(shapeSize, shape, shapeId, size);
            return shape;
        }

        private void InitializeShape(ShapeSize shapeSize, Shape shape, string newShapeId, float size)
        {
            shape.Construct(shapeSize, this, _progressService,
                () => _saveLoadService.RemoveUpdatebleProgress(shape),
                newShapeId);
            shape.transform.localScale = new Vector2(size, size);
            _saveLoadService.AddUpdatebleProgress(shape);
        }
    }
}