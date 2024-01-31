using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Services.InputService
{
    public class MobileInput : MonoBehaviour, IInput, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public event Action OnDrop;
        
        private bool _isPressed = true;
        private float _positionX;

        public float GetXPosition() => _positionX;
        public bool IsPress() => _isPressed == true;
        public bool IsDropped() => _isPressed == false;

        public void OnDrag(PointerEventData eventData)
        {
            if (Camera.main is not null) 
                _positionX = Camera.main.ScreenToWorldPoint(eventData.position).x;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isPressed = true;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _isPressed = false;
            
            OnDrop?.Invoke();
        }
    }
}