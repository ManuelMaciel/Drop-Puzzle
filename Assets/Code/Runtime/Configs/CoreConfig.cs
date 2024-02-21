using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "CoreConfig", menuName = "Configs/CoreConfig", order = 0)]
    public class CoreConfig : ScriptableObject
    {
        [FormerlySerializedAs("GameplayAssetsConfig")] public GameplayConfig gameplayConfig;
        public WindowAssetsConfig WindowAssetsConfig;
        public AudioConfig AudioConfig;
        public PurchasedBackgroundsConfig PurchasedBackgroundsConfig;
        public ShapeScoreConfig ShapeScoreConfig;
        public ShapeSizeConfig ShapeSizeConfig;
    }
}