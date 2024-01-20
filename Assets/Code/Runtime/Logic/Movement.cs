using System;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class Movement : MonoBehaviour
    {
        public event Action OnShapeDropped;

        private ScreenBorders _screenBorders;
        private Rigidbody2D _shapeRigidbody;
        private IInput _input;
        private bool _isDropped;
        private float _halfSize;
        
        [Inject]
        public void Construct(IInput input)
        {
            _input = input;
        }

        private void Awake() => 
            _screenBorders = new ScreenBorders();

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

            if (_input.IsDropped())
            {
                OnShapeDropped?.Invoke();

                _shapeRigidbody.bodyType = RigidbodyType2D.Dynamic;
                _isDropped = true;
            }
        }

        public void AddShape(Rigidbody2D newShape)
        {
            Collider2D shapeCollider = newShape.GetComponent<Collider2D>();

            _halfSize = shapeCollider.bounds.extents.x;
            _shapeRigidbody = newShape;
            _isDropped = false;
        }
    }
}