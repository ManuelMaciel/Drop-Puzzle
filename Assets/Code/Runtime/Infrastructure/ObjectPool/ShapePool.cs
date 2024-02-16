using System;
using Code.Runtime.Logic;
using Zenject;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class ShapePool : GameObjectPoolResolver<Shape>
    {
        private readonly Action<Shape> _onCreateShape;

        public ShapePool(Shape @object, int preloadCount, IGameObjectsPoolContainer gameObjectsPoolContainer,
            DiContainer diContainer, Action<Shape> onCreateShape) : base(@object,
            preloadCount, gameObjectsPoolContainer, diContainer)
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