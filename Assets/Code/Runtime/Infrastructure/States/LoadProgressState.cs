using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Repositories;
using Code.Services.Progress;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ShapeScoreConfig _shapeScoreConfig;

        LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService,
            ShapeScoreConfig shapeScoreConfig)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
            _shapeScoreConfig = shapeScoreConfig;
        }

        public void Enter()
        {
            ProgressInitializer progressInitializer =
                new ProgressInitializer(_persistentProgressService.InteractorContainer, _shapeScoreConfig);
            
            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Gameplay.ToString());
        }

        public void Exit()
        {
        }
    }
}