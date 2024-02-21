using Code.Runtime.Logic.Gameplay;
using UnityEngine;

namespace Code.Runtime.Logic.Factories
{
    public interface IShapeFactory
    {
        Shape CreateShape(Vector3 at, ShapeSize shapeSize, bool isDropped = false);
        Shape CreateShapeFromLoadedData(Vector3 at, ShapeSize shapeSize, string shapeId);
    }
}