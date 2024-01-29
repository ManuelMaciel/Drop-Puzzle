using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Services.Progress;
using CodeBase.Services.StaticDataService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;
        private ShapeSizeConfig _shapeSizeConfig;
        private Shape _shapePrefab;

        public ShapeFactory(IStaticDataService staticDataService, IPersistentProgressService progressService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
        }

        public void Initialize()
        {
            _shapeSizeConfig = _staticDataService.ShapeSizeConfig;
            _shapePrefab = _shapeSizeConfig.ShapePrefab;
        }

        public Shape CreateShape(Vector3 at, ShapeSize shapeSize, bool isDropped = false)
        {
            float size = _shapeSizeConfig.Sizes[(int) shapeSize];
            Shape shape = Object.Instantiate(_shapePrefab, at, Quaternion.identity);

            shape.Construct(shapeSize, this, _progressService);
            shape.transform.localScale = new Vector2(size, size);

            if (isDropped)
                _progressService.InteractorContainer.Get<GameplayShapesInteractor>().AddShape(shape);

            return shape;
        }
    }
}