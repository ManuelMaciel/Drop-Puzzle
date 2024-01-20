using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;

        LoadLevelState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, () => { });
        }
        
        public void Exit()
        {
            
        }
    }
}