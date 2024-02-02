using Code.Runtime.Infrastructure;
using Code.Runtime.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI
{
    public class ChangeSceneButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private SceneName nextScene;

        private ISceneLoader _sceneLoader;

        [Inject]
        void Construct(ISceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        private void OnEnable() => 
            button.onClick.AddListener(ChangeState);

        private void OnDisable() => 
            button.onClick.RemoveListener(ChangeState);

        private void ChangeState() => 
            _sceneLoader.Load(nextScene.ToString());
    }
}