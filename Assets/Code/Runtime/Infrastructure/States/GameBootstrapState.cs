using CodeBase.Services.LogService;

namespace Code.Runtime.Infrastructure.States
{
    public class GameBootstrapState : IState
    {
        private readonly ILogService _logService;
        private readonly ISceneLoader _sceneLoader;

        public GameBootstrapState(ILogService logService, SceneLoader sceneLoader)
        {
            _logService = logService;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _logService.Log("Rabotaet");
            
            _sceneLoader.Load("Bootstrap", ToNextState);
        }

        private void ToNextState()
        {
            _logService.Log("To next state");
        }

        public void Exit()
        {
            
        }
    }
}