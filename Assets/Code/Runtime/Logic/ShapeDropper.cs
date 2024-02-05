using System;
using Code.Runtime.Interactors;
using Code.Services.Progress;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(Movement))]
    public class ShapeDropper : MonoBehaviour
    {
        public event Action OnShapeDropped;

        private Shape _currentShape;
        private Movement _shapeMovement;
        private Rigidbody2D _shapeRigidbody;
        private Collider2D _shapeCollider;
        private GameplayShapesInteractor _gameplayShapesInteractor;
        private IPersistentProgressService _progressService;
        private IInput _input;

        [Inject]
        public void Construct(IInput input, IPersistentProgressService progressService)
        {
            _input = input;
            _progressService = progressService;
        }

        private void Start() =>
            _gameplayShapesInteractor = _progressService.InteractorContainer.Get<GameplayShapesInteractor>();

        private void OnEnable() => 
            _input.OnDrop += Drop;

        private void OnDisable() => 
            _input.OnDrop -= Drop;

        public void Initialize() => 
            _shapeMovement = this.GetComponent<Movement>();

        public void AddShape(Shape newShape)
        {
            _shapeCollider = newShape.GetComponent<Collider2D>();
            _shapeRigidbody = newShape.GetComponent<Rigidbody2D>();
            _currentShape = newShape;

            _shapeRigidbody.bodyType = RigidbodyType2D.Kinematic;
            _shapeMovement.AddShape(_shapeRigidbody, _shapeCollider);
            _shapeCollider.enabled = false;
            _shapeCollider.transform.SetParent(this.transform);
        }

        private void Drop()
        {
            if(_currentShape == null) return;
            
            OnShapeDropped?.Invoke();

            _shapeCollider.enabled = true;
            _shapeMovement.RemoveShape();
            _gameplayShapesInteractor.AddShape(_currentShape);
            _shapeRigidbody.bodyType = RigidbodyType2D.Dynamic;
            _shapeCollider.transform.SetParent(this.transform.root);

            _currentShape = null;
        }
    }
}