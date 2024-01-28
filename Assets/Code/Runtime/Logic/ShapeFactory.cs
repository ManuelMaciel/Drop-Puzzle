using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Services.Progress;
using CodeBase.Services.StaticDataService;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ShapeSizeConfig _shapeSizeConfig;
        private readonly Shape _shapePrefab;

        public ShapeFactory(IStaticDataService staticDataService, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _shapeSizeConfig = staticDataService.ShapeSizeConfig;

            Debug.Log(staticDataService.ShapeSizeConfig);
            _shapePrefab = _shapeSizeConfig.ShapePrefab;
        }

        public Shape CreateShape(Vector3 at, ShapeSize shapeSize)
        {
            float size = _shapeSizeConfig.Sizes[(int) shapeSize];
            Shape shape = Object.Instantiate(_shapePrefab, at, Quaternion.identity);
            ScoreInteractor scoreInteractor = _progressService.InteractorContainer.Get<ScoreInteractor>();
            ShapeInteractor shapeInteractor = _progressService.InteractorContainer.Get<ShapeInteractor>();
            
            shape.Construct(shapeSize, this, scoreInteractor, shapeInteractor);
            shape.transform.localScale = new Vector2(size, size);

            return shape;
        }
    }
}