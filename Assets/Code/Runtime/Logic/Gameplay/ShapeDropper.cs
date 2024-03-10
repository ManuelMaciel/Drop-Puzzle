using System;
using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Runtime.Services.AudioService;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Progress;
using UnityEngine;
using Zenject;
// using DG.Tweening;

namespace Code.Runtime.Logic.Gameplay
{
    [RequireComponent(typeof(ShapeMovement))]
    public class ShapeDropper : MonoBehaviour
    {
        public event Action OnShapeDropped;

        private Shape _currentShape;
        private ShapeMovement _shapeShapeMovement;
        private Rigidbody2D _shapeRigidbody;
        private Collider2D _shapeCollider;
        private GameplayShapesInteractor _gameplayShapesInteractor;
        private IPersistentProgressService _progressService;
        private IInput _input;
        private IAudioService _audioService;

        [Inject]
        public void Construct(IInput input, IPersistentProgressService progressService, IAudioService audioService)
        {
            _audioService = audioService;
            _input = input;
            _progressService = progressService;
        }

        private void Start() =>
            _gameplayShapesInteractor = _progressService.InteractorContainer.Get<GameplayShapesInteractor>();

        private void OnEnable() =>
            _input.OnDrop += Drop;

        private void OnDisable()
        {
            if (_currentShape != null)
                _currentShape.gameObject.SetActive(false);

            _input.OnDrop -= Drop;
        }

        public void Initialize() =>
            _shapeShapeMovement = this.GetComponent<ShapeMovement>();

        public void AddShape(Shape newShape)
        {
            _shapeCollider = newShape.GetComponent<Collider2D>();
            _shapeRigidbody = newShape.GetComponent<Rigidbody2D>();
            _currentShape = newShape;

            _shapeRigidbody.bodyType = RigidbodyType2D.Kinematic;
            _shapeShapeMovement.AddShape(_shapeRigidbody, _shapeCollider);
            _shapeCollider.enabled = false;
        }

        private void Drop()
        {
            if (_currentShape == null) return;

            OnShapeDropped?.Invoke();

            _audioService.PlaySfx(SfxType.DropShape);
            _shapeCollider.enabled = true;
            _shapeShapeMovement.RemoveShape();
            _gameplayShapesInteractor.AddShape(_currentShape);
            _shapeRigidbody.bodyType = RigidbodyType2D.Dynamic;

            _currentShape = null;
        }
    }
}