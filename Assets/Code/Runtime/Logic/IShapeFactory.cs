using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Runtime
{
    public interface IShapeFactory
    {
        Shape CreateShape(Vector3 at, ShapeSize shapeSize);
        void Initialize();
    }
}