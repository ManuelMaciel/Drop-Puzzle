using Code.Runtime.Infrastructure.StateMachines;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        LoadProgressState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Gameplay.ToString());
        }

        public void Exit()
        {
            
        }
    }
}