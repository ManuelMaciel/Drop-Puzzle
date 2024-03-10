using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "AdConfig", menuName = "Configs/AdConfig")]
    public class AdConfig : ScriptableObject
    {
        [Min(0)] public int Reward;
        public bool TestMode = true;
        public string AndroidGameId = "5522750";
        public string IOSGameId = "5522751";
        public string UnityRewardedVideoIdAndroid = "Rewarded_Android";
        public string UnityRewardedVideoIdIOS = "Rewarded_iOS";
    }
}