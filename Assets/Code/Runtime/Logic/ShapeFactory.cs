using Code.Runtime.Configs;
using Code.Runtime.Logic;
using Code.Runtime.Repositories;
using Code.Services.Progress;
using UnityEngine;
using Zenject;

namespace Code.Runtime
{
    public class ShapeFactory : IShapeFactory
    {
        [Inject] private IPersistentProgressService _progressService;

        private readonly ShapeSizeConfig _shapeSizeConfig;
        private readonly Shape _shapePrefab;

        public ShapeFactory(ShapeSizeConfig shapeSizeConfig, Shape shapePrefab)
        {
            _shapeSizeConfig = shapeSizeConfig;
            _shapePrefab = shapePrefab;
        }

        public Shape CreateShape(Vector3 at, ShapeSize shapeSize)
        {
            float size = _shapeSizeConfig.Sizes[(int) shapeSize];
            Shape shape = Object.Instantiate(_shapePrefab, at, Quaternion.identity);

            shape.Construct(shapeSize, this, _progressService.InteractorContainer.Get<ScoreInteractor>());
            shape.transform.localScale = new Vector2(size, size);

            return shape;
        }
    }
}