using Code.Runtime.Infrastructure.StateMachines;
using CodeBase.Services.StaticDataService;

namespace Code.Runtime.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(ISceneLoader sceneLoader, IStaticDataService staticDataService,
            GameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _staticDataService.Initialize();

            _sceneLoader.Load(SceneName.Bootstrap.Index(), ToLoadProgressState);
        }

        private void ToLoadProgressState() =>
            _gameStateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
    }
}