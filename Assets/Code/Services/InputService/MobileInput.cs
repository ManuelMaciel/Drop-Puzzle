using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Services.InputService
{
    public class MobileInput : MonoBehaviour, IInput, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public event Action OnDrop;
        
        private bool _isPressed = false;
        private Vector3 _touchPosition;

        public float GetXPosition()
        {
            return GetPosition().x;
        }

        public Vector2 GetPosition()
        {
            if (_isPressed)
                return Camera.main.ScreenToWorldPoint(_touchPosition);
            else
                return new Vector2();
        }

        public bool IsPress() => _isPressed == true;
        public bool IsDropped() => _isPressed == false;
        
        public void OnDrag(PointerEventData eventData)
        {
            _touchPosition = eventData.position;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _touchPosition = eventData.position;
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            
            OnDrop?.Invoke();
        }
    }
}