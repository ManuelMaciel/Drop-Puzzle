using Code.Runtime.Infrastructure.StateMachines;

namespace Code.Runtime.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(SceneLoader sceneLoader, GameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            _sceneLoader.Load(SceneName.Bootstrap.Index(), ToLoadProgressState);
        }

        private void ToLoadProgressState() => 
            _gameStateMachine.Enter<LoadProgressState>();
        
        public void Exit()
        {
            
        }
    }
}