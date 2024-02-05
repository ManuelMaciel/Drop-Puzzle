using System;
using Code.Runtime.Logic;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "ShapeSizeConfig", menuName = "Configs/ShapeSizeConfig", order = 0)]
    public class ShapeSizeConfig : ScriptableObject
    {
        public Shape ShapePrefab;
        public ShapeType ShapeType;
        public float[] Sizes;
        public Sprite[] Sprites;

        private void OnValidate()
        {
            int sizes = Enum.GetNames(typeof(ShapeSize)).Length;
            if (Sizes.Length < sizes) 
                Sizes = new float[sizes];

            if (Sprites.Length < sizes) 
                Sprites = new Sprite[sizes];
        }
    }
}