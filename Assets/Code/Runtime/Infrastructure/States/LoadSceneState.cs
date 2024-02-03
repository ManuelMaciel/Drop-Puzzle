using Code.Services.SaveLoadService;
using CodeBase.Services.LogService;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadSceneState : IPayloadedState<string>
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
            
            _sceneLoader.Load(sceneName,
                () => { _logService.Log($"Loaded: {sceneName} (Scene)"); });
        }

        public void Exit()
        {
        }
    }
}