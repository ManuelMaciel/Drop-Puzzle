using UnityEngine;

namespace Code.Runtime.Logic
{
    public class ScreenBorders
    {
        public float LeftSide { get; private set; }
        public float RightSide { get; private set; }

        public ScreenBorders()
        {
            GetBorders();
        }

        private void GetBorders()
        {
            LeftSide = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            RightSide = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        }
    }
}