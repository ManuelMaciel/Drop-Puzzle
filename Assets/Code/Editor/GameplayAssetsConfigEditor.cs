using Code.Runtime.Configs;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(GameplayConfig))]
    public class GameplayAssetsConfigEditor : UnityEditor.Editor
    {
        private const string SpawnPointTag = "SpawnPoint";
        
        private GameplayConfig _gameplayConfig;

        void OnEnable()
        {
            _gameplayConfig = (GameplayConfig)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Find Spawn Point Position"))
            {
                GameObject spawnPoint = GameObject.FindGameObjectWithTag(SpawnPointTag);

                if(spawnPoint == null) return;
                
                _gameplayConfig.SpawnPointPosition = spawnPoint.transform.position;
            }
        }
    }
}