using Code.Runtime.Interactors;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class Shape : MonoBehaviour
    {
        public ShapeSize ShapeSize { get; private set; }
        public bool IsCombined { get; private set; }

        [SerializeField] private ShapeType _shapeType;

        private IShapeFactory _shapeFactory;
        private ShapeInteractor _shapeInteractor;

        public void Construct(ShapeSize shapeSize, IShapeFactory shapeFactory, ShapeInteractor shapeInteractor)
        {
            _shapeInteractor = shapeInteractor;
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
                    _shapeInteractor.ShapeCombined();
                }
            }
        }
    }
}