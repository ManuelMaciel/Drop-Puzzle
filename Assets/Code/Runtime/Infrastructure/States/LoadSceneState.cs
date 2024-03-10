using Code.Runtime.Services.LogService;
using Code.Runtime.Services.SaveLoadService;
using DG.Tweening;
using Plugin.DocuFlow.Documentation;

namespace Code.Runtime.Infrastructure.States
{
    [Doc("The LoadSceneState class responsible for loading a specific scene.")]
    public class LoadSceneState : IPayloadedState<string>, IReExitableState
    {
        private readonly ILogService _logService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ISaveLoadService _saveLoadService;

        public LoadSceneState(ILogService logService, ISceneLoader sceneLoader, ISaveLoadService saveLoadService)
        {
            _logService = logService;
            _sceneLoader = sceneLoader;
            _saveLoadService = saveLoadService;
        }

        public void Enter(string sceneName)
        {
            _saveLoadService.SaveProgress();
            _saveLoadService.ClearUpdatebleProgress();

            DOTween.KillAll();

            _sceneLoader.Load(sceneName,
                () => { _logService.Log($"Loaded: {sceneName} (Scene)"); });
        }

        public void Exit()
        {
        }
    }
}