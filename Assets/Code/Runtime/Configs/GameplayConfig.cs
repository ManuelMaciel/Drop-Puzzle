using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "GameplayAssetsConfig", menuName = "Configs/GameplayAssetsConfig")]
    public class GameplayConfig : ScriptableObject
    {
        public GameObject HUD;
        public GameObject ShapeDropper;
        [Range(0f, 5f)] public float DropInterval = 1f;
        public Vector3 SpawnPointPosition;
    }
}