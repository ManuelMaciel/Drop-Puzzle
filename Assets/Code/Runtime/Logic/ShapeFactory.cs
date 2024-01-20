using Code.Runtime.Configs;
using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Runtime
{
    public class ShapeFactory : IShapeFactory
    {
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
            
            shape.Construct(shapeSize, this);
            shape.transform.localScale = new Vector2(size, size);

            return shape;
        }
    }
}