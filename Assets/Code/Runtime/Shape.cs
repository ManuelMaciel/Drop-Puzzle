using UnityEngine;

namespace Code.Runtime
{
    public class Shape : MonoBehaviour
    {
        public ShapeSize ShapeSize { get; private set; }
        public bool IsCombined { get; private set; }

        [SerializeField] private ShapeType _shapeType;

        private IShapeFactory _shapeFactory;

        public void Construct(ShapeSize shapeSize, IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
            ShapeSize = shapeSize;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Shape shape))
            {
                if (!shape.IsCombined && shape.ShapeSize == this.ShapeSize)
                {
                    Vector3 spawnPosition = this.transform.position;
                    IsCombined = true;

                    Destroy(this.gameObject);
                    Destroy(other.gameObject);

                    _shapeFactory.CreateShape(spawnPosition, ShapeSize.NextSize());
                }
            }
        }
    }
}