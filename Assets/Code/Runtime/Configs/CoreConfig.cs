using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Configs
{
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