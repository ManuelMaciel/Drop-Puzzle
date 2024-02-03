using System;

namespace Code.Runtime.Logic
{
    public interface IShapeDeterminantor
    {
        event Action OnShapeChanged;
        ShapeSize CurrentShapeSize { get; }
        ShapeSize NextShapeSize { get; }
        ShapeSize GetShape();
    }
}