using UnityEngine;

namespace Code.Runtime.Logic
{
    public interface IShapeFactory
    {
        Shape CreateShape(Vector3 at, ShapeSize shapeSize, bool isDropped = false);
        void Initialize();
    }
}