using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "GameplayAssetsConfig", menuName = "Configs/GameplayAssetsConfig")]
    public class GameplayAssetsConfig : ScriptableObject
    {
        public GameObject HUD;
        public GameObject ShapeDropper;
        public Vector3 SpawnPointPosition;
    }
}