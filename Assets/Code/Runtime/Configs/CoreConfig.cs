using Plugin.DocuFlow.Documentation;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [Doc("The CoreConfig class represents a ScriptableObject containing various core configurations used in the game. It provides configurations for gameplay, advertisements, animations, window assets, audio settings, purchased backgrounds, shape scores, and shapes. This ScriptableObject is typically created and configured in the Unity Editor and serves as a centralized repository for core game configurations.")]
    [CreateAssetMenu(fileName = "CoreConfig", menuName = "Configs/CoreConfig", order = -1)]
    public class CoreConfig : ScriptableObject
    {
        public GameplayConfig GameplayConfig;
        public AdConfig AdConfig;
        public AnimationConfig AnimationConfig;
        public WindowAssetsConfig WindowAssetsConfig;
        public AudioConfig AudioConfig;
        public PurchasedBackgroundsConfig PurchasedBackgroundsConfig;
        public ShapeScoreConfig ShapeScoreConfig;
        public ShapeConfig shapeConfig;
    }
}