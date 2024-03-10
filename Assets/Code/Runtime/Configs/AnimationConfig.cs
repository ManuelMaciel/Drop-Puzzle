using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "AnimationConfig", menuName = "Configs/AnimationConfig")]
    public class AnimationConfig : ScriptableObject
    {
        public float BlinkDelay;
        public Vector2 MinMaxBlinkShapes;
        public float PunchAnimationScaleFactor = 0.2f;
        public float PunchAnimationScaleDuration = 0.2f;
        
        public Vector3 GetPunchAnimationScaleFactor() => Vector3.one * PunchAnimationScaleFactor;
    }
}