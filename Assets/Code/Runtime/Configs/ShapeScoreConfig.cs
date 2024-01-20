using System;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "ShapeScoreConfig", menuName = "Configs/ShapeScoreConfig")]
    public class ShapeScoreConfig : ScriptableObject
    {
        public int[] Scores;
        
        private void OnValidate()
        {
            int sizes = Enum.GetNames(typeof(ShapeSize)).Length;
            if (Scores.Length < sizes)
            {
                Scores = new int[sizes];
            }
        }
    }
}