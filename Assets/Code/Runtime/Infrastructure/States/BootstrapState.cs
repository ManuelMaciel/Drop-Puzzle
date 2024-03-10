using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Services.AdsService;
using Code.Runtime.Services.StaticDataService;
using Code.Runtime.Services.WindowsService;
using Code.Runtime.UI;
using Plugin.DocuFlow.Documentation;

namespace Code.Runtime.Infrastructure.States
{
    [Doc("The BootstrapState class responsible for bootstrapping the game. It initializes essential services and components required for the game's functionality during the startup phase.")]
    public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IWindowService _windowService;
        private readonly IUIFactory _uiFactory;
        private IAdsService _adsService;

        public BootstrapState(ISceneLoader sceneLoader, IStaticDataService staticDataService,
            GameStateMachine gameStateMachine, IWindowService windowService, IUIFactory uiFactory, IAdsService adsService)
        {
            _adsService = adsService;
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
            _adsService.Initialize();

            _sceneLoader.Load(SceneName.Bootstrap.ToString(), ToLoadProgressState);
        }

        private void ToLoadProgressState() =>
            _gameStateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
    }
}