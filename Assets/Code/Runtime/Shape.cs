using UnityEngine;

namespace Code.Runtime
{
    public class Shape : MonoBehaviour
    {
        [SerializeField] private ShapeType _shapeType;

        public ShapeSize ShapeSize { get; private set; }

        public void Initialize(ShapeSize shapeSize)
        {
            ShapeSize = shapeSize;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Shape shape))
            {
                if (shape.ShapeSize == this.ShapeSize)
                {
                    
                }
            }
        }
    }
}