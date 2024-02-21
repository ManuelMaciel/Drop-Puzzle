using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Services.StaticDataService;
using Code.Runtime.Services.WindowsService;
using Code.Runtime.UI;

namespace Code.Runtime.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IWindowService _windowService;
        private readonly IUIFactory _uiFactory;

        public BootstrapState(ISceneLoader sceneLoader, IStaticDataService staticDataService,
            GameStateMachine gameStateMachine, IWindowService windowService, IUIFactory uiFactory)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _staticDataService.Initialize();
            _uiFactory.Initialize();
            _windowService.Initialize();

            _sceneLoader.Load(SceneName.Bootstrap.ToString(), ToLoadProgressState);
        }

        private void ToLoadProgressState() =>
            _gameStateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
    }
}