using System;
using Code.Runtime.Logic;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class ShapePool : GameObjectPool<Shape>
    {
        private readonly Action<Shape> _onCreateShape;

        public ShapePool(Shape @object, int preloadCount, IGlobalGameObjectPool globalGameObjectPool, Action<Shape> onCreateShape) : base(@object,
            preloadCount, globalGameObjectPool)
        {
            _onCreateShape = onCreateShape;
        }

        protected override Shape PreloadAction()
        {
            Shape shape = base.PreloadAction();

            _onCreateShape?.Invoke(shape);

            return shape;
        }
    }
}