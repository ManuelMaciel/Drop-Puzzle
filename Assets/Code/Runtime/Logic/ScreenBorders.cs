using UnityEngine;

namespace Code.Runtime.Logic
{
    public class ScreenBorders
    {
        public float LeftSide => _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        public float RightSide => _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        private Camera _camera;
        
        public ScreenBorders()
        {
            _camera = Camera.main;
            
            if(_camera == null)
                Debug.LogError("There is no main camera on scene");
        }
    }
}