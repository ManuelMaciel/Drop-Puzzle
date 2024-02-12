using System;
using Code.Runtime.Extensions;
using Code.Runtime.Interactors;
using Code.Services.Progress;
using UnityEngine;

namespace Code.Runtime.Logic
{
    public class Shape : MonoBehaviour, IUpdatebleProgress
    {
        public ShapeSize ShapeSize { get; private set; }
        public bool IsCombined { get; private set; }
        public string ShapeId { get; private set; }

        [SerializeField] private ShapeType _shapeType;

        private IShapeFactory _shapeFactory;
        private ShapeInteractor _shapeInteractor;
        private ScoreInteractor _scoreInteractor;
        private GameplayShapesInteractor _gameplayShapesInteractor;
        private Rigidbody2D _rigidbody;
        private Action _onDestroyed;

        private void Start()
        {
            _rigidbody = this.GetComponent<Rigidbody2D>();
        }

        public void Construct(ShapeSize shapeSize, IShapeFactory shapeFactory,
            IPersistentProgressService progressService, Action onDestroyed, string shapeId = null)
        {
            _onDestroyed = onDestroyed;
            _scoreInteractor = progressService.InteractorContainer.Get<ScoreInteractor>();
            _shapeInteractor = progressService.InteractorContainer.Get<ShapeInteractor>();
            _gameplayShapesInteractor = progressService.InteractorContainer.Get<GameplayShapesInteractor>();

            _shapeFactory = shapeFactory;
            ShapeSize = shapeSize;
            ShapeId = shapeId;
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
            _rigidbody.inertia = 0f;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            _onDestroyed?.Invoke();
        }

        public void UpdateProgress(IPersistentProgressService persistentProgressService) =>
            persistentProgressService.InteractorContainer.Get<GameplayShapesInteractor>().UpdateShapeData(this);
    }
}