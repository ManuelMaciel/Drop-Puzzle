using System;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "ShapeSizeConfig", menuName = "Configs/ShapeSizeConfig", order = 0)]
    public class ShapeSizeConfig : ScriptableObject
    {
        [FormerlySerializedAs("shapeBasePrefab")] [FormerlySerializedAs("ShapePrefab")] public Shape shapePrefab;
        public ShapeType ShapeType;
        public float[] Sizes;
        public Sprite[] Sprites;
        public Sprite[] BlinkSprites;

        private void OnValidate()
        {
            int sizes = Enum.GetNames(typeof(ShapeSize)).Length;
            if (Sizes.Length < sizes) 
                Sizes = new float[sizes];

            if (Sprites.Length < sizes) 
                Sprites = new Sprite[sizes];
            
            if (Sprites.Length < sizes) 
                BlinkSprites = new Sprite[sizes];
        }

        public int ShapesCount() =>
            Sizes.Length;
    }
}