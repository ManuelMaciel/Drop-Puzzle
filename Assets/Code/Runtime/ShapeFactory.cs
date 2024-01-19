using Code.Runtime.Configs;
using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Runtime
{
    public class ShapeFactory : MonoBehaviour, IShapeFactory
    {
        public ShapeSizeConfig ShapeSizeConfig;
        public Shape shapePrefab;
        
        public Shape CreateShape(Vector3 at, ShapeSize shapeSize)
        {
            float size = ShapeSizeConfig.Sizes[(int) shapeSize];
            Shape shape = Object.Instantiate(shapePrefab, at, Quaternion.identity);
            
            shape.Construct(shapeSize, this);
            shape.transform.localScale = new Vector2(size, size);

            return shape;
        }
    }
}