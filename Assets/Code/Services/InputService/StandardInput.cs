using System;
using UnityEngine;

namespace Code.Services.InputService
{
    public class StandardInput : IInput
    {
        public event Action OnDrop;

        public float GetXPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            
            return Camera.main.ScreenToWorldPoint(mousePosition).x;
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