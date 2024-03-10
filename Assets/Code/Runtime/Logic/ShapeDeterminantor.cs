using System;
using Plugin.DocuFlow.Documentation;
using Random = UnityEngine.Random;

namespace Code.Runtime.Logic
{
    [Doc("The ShapeDeterminantor class is responsible for determining the size of shapes in the game. It randomly selects the size of the current shape and precomputes the size of the next shape. It also provides an event for notifying when the shape size changes.")]
    public class ShapeDeterminantor : IShapeDeterminantor
    {
        public const int MaxRandomShapeSize = 3;
        
        public event Action OnShapeChanged;

        public ShapeSize CurrentShapeSize { get; private set; }
        public ShapeSize NextShapeSize { get; private set; }

        private int _lastRandomSizeIndex;
        
        public ShapeDeterminantor()
        {
            CurrentShapeSize = GetRandomShape();
            NextShapeSize = GetRandomShape();
        }

        public ShapeSize GetShape()
        {
            ShapeSize shapeSize = CurrentShapeSize;

            SetNextShapeSize();
            
            return shapeSize;
        }

        private void SetNextShapeSize()
        {
            OnShapeChanged?.Invoke();
            
            CurrentShapeSize = NextShapeSize;
            NextShapeSize = GetRandomShape();
        }

        private ShapeSize GetRandomShape()
        {
            int randomIndex = 0;

            do
            {
                randomIndex = Random.Range(0, MaxRandomShapeSize);
            } while (_lastRandomSizeIndex == randomIndex);

            return (ShapeSize) (_lastRandomSizeIndex = randomIndex);
        }
    }
}