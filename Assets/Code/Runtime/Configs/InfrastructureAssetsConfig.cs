using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "InfrastructureAssetsConfig", menuName = "Configs/InfrastructureAssetsConfig")]
    public class InfrastructureAssetsConfig : ScriptableObject
    {
        public GameObject GameBootstrapper;
        public GameObject CoroutineRunner;
    }
}