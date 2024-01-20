using Code.Runtime.Repositories;
using Code.Services.Progress;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IPersistentProgressService _persistentProgressService;

        LoadLevelState(SceneLoader sceneLoader, IPersistentProgressService persistentProgressService)
        {
            _sceneLoader = sceneLoader;
            _persistentProgressService = persistentProgressService;
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