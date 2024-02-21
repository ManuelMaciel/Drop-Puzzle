using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "CoreConfig", menuName = "Configs/CoreConfig", order = 0)]
    public class CoreConfig : ScriptableObject
    {
        public GameplayAssetsConfig GameplayAssetsConfig;
        public InfrastructureAssetsConfig InfrastructureAssetsConfig; 
        public WindowAssetsConfig WindowAssetsConfig;
        public AudioConfig AudioConfig;
        public PurchasedBackgroundsConfig PurchasedBackgroundsConfig;
        public ShapeScoreConfig ShapeScoreConfig;
        public ShapeSizeConfig ShapeSizeConfig;
    }
}