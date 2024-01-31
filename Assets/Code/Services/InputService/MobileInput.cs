using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Services.InputService
{
    public class MobileInput : MonoBehaviour, IInput, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private float _positionX;
        private bool _isPressed;

        public float GetXPosition() => _positionX;
        public bool IsPress() => _isPressed == true;
        public bool IsDropped() => _isPressed == false;
        
        public void OnDrag(PointerEventData eventData)
        {
            _positionX = eventData.position.x;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isPressed = false;
        }
    }
}