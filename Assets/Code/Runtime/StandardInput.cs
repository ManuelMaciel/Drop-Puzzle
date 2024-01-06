using UnityEngine;

namespace Code.Runtime
{
    public class StandardInput : IInput
    {
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
            return Input.GetMouseButtonUp(0);
        }
    }
}