using Code.Runtime.Interactors;
using UnityEngine;

namespace Code.Runtime.Logic.Gameplay
{
    public interface IShapeBase : IUpdatebleProgress
    {
        ShapeSize ShapeSize { get; }
        string ShapeId { get; }
        Transform transform { get; }
    }
}