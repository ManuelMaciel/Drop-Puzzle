using System;
using UnityEngine;

namespace Code.Runtime.Services.InputService
{
    public class StandardInput : IInput
    {
        public event Action OnDrop;

        public float GetXPosition()
        {
            return GetPosition().x;
        }

        public Vector2 GetPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }

        public bool IsPress()
        {
            return Input.GetMouseButton(0);
        }

        public bool IsDropped()
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnDrop?.Invoke();

                return true;
            }

            return false;
        }
    }
}