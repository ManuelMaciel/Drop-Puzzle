using System;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "ShapeSizeConfig", menuName = "Configs/ShapeSizeConfig", order = 0)]
    public class ShapeSizeConfig : ScriptableObject
    {
        public ShapeType ShapeType;
        public float[] Sizes;

        private void OnValidate()
        {
            int sizes = Enum.GetNames(typeof(ShapeSize)).Length;
            if (Sizes.Length < sizes)
            {
                Sizes = new float[sizes];
            }
        }
    }
}