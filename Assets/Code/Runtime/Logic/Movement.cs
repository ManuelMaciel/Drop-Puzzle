using Code.Runtime.Interactors;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class Movement : MonoBehaviour
    {
        private ScreenBorders _screenBorders;
        private Rigidbody2D _shapeRigidbody;

        private IInput _input;
        
        private float _halfSize;
        private GameplayShapesInteractor _gameplayShapesInteractor;

        [Inject]
        public void Construct(IInput input) => 
            _input = input;

        private void Awake() =>
            _screenBorders = new ScreenBorders();
        
        private void Update()
        {
            if(_shapeRigidbody == null) return;

            if (_input.IsPress())
            {
                float clampXPosition = Mathf.Clamp(_input.GetXPosition(),
                    _screenBorders.LeftSide + _halfSize,
                    _screenBorders.RightSide - _halfSize);
                
                _shapeRigidbody.position = new Vector2(clampXPosition,
                    _shapeRigidbody.position.y);
            }
        }
        
        public void AddShape(Rigidbody2D shapeRigidbody, Collider2D shapeCollider)
        {
            _shapeRigidbody = shapeRigidbody;
            _halfSize = shapeCollider.bounds.extents.x;
        }

        public void RemoveShape()
        {
            _shapeRigidbody = null;
        }
    }
}