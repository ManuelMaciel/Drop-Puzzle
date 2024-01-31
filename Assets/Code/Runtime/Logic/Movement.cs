using System;
using Code.Runtime.Interactors;
using Code.Services.Progress;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class Movement : MonoBehaviour
    {
        public event Action OnShapeDropped;

        private ScreenBorders _screenBorders;
        private Rigidbody2D _shapeRigidbody;
        private Shape _currentShape;
        
        private IPersistentProgressService _progressService;
        private IInput _input;

        private bool _isDropped;
        private float _halfSize;
        private GameplayShapesInteractor _gameplayShapesInteractor;

        [Inject]
        public void Construct(IInput input, IPersistentProgressService progressService)
        {
            _input = input;
            _progressService = progressService;
        }

        private void Awake() => 
            _screenBorders = new ScreenBorders();

        private void Start() => 
            _gameplayShapesInteractor = _progressService.InteractorContainer.Get<GameplayShapesInteractor>();

        private void OnEnable() => 
            _input.OnDrop += Drop;

        private void OnDisable() => 
            _input.OnDrop -= Drop;

        private void Update()
        {
            if(_isDropped) return;

            if (_input.IsPress())
            {
                float clampXPosition = Mathf.Clamp(_input.GetXPosition(),
                    _screenBorders.LeftSide + _halfSize,
                    _screenBorders.RightSide - _halfSize);
                
                _shapeRigidbody.position = new Vector2(clampXPosition, 
                    _shapeRigidbody.position.y);
            }
        }

        private void Drop()
        {
            OnShapeDropped?.Invoke();
                
            _gameplayShapesInteractor.AddShape(_currentShape);
            _shapeRigidbody.bodyType = RigidbodyType2D.Dynamic;
            _isDropped = true;
        }

        public void AddShape(Shape newShape)
        {
            Collider2D shapeCollider = newShape.GetComponent<Collider2D>();
            
            _shapeRigidbody = newShape.GetComponent<Rigidbody2D>();
            _shapeRigidbody.bodyType = RigidbodyType2D.Kinematic;
            _halfSize = shapeCollider.bounds.extents.x;
            _currentShape = newShape;
            _isDropped = false;
        }
    }
}