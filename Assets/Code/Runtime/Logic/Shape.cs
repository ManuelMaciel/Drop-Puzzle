using Code.Runtime.Extensions;
using Code.Runtime.Interactors;
using Code.Services.Progress;
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
        private ScoreInteractor _scoreInteractor;
        private GameplayShapesInteractor _gameplayShapesInteractor;

        public void Construct(ShapeSize shapeSize, IShapeFactory shapeFactory, IPersistentProgressService progressService)
        {
            _scoreInteractor = progressService.InteractorContainer.Get<ScoreInteractor>();
            _shapeInteractor = progressService.InteractorContainer.Get<ShapeInteractor>();
            _gameplayShapesInteractor = progressService.InteractorContainer.Get<GameplayShapesInteractor>();
            
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

                    DestroyShape(this);
                    DestroyShape(shape);

                    _shapeFactory.CreateShape(spawnPosition, ShapeSize.NextSize(), true);
                    _scoreInteractor.AddScoreByShapeSize(ShapeSize);
                    _shapeInteractor.ShapeCombined();
                }
            }
        }

        private void DestroyShape(Shape shape)
        {
            _gameplayShapesInteractor.RemoveShape(shape);
            Destroy(shape.gameObject);
        }
    }
}