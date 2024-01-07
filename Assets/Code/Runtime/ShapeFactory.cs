using Code.Runtime.Configs;
using UnityEngine;

namespace Code.Runtime
{
    public class ShapeFactory : MonoBehaviour
    {
        public ShapeSizeConfig ShapeSizeConfig;
        public GameObject shapePrefab;
        
        private GameObject CreateShape(Vector3 at, ShapeSize shapeSize)
        {
            float size = ShapeSizeConfig.Sizes[(int) shapeSize];
            GameObject shape = Object.Instantiate(shapePrefab, at, Quaternion.identity);

            shape.transform.localScale = new Vector2(size, size);

            return shape;
        }
    }
}