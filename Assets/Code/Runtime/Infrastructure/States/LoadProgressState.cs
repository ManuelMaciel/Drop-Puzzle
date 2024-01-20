using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Repositories;
using Code.Services.Progress;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _persistentProgressService;

        LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter()
        {
            ScoreRepository scoreRepository = new ScoreRepository();
            ScoreInteractor scoreInteractor = new ScoreInteractor(scoreRepository);
            
            scoreInteractor.AddScore(5);
            
            _persistentProgressService.Interactors.Register(scoreInteractor);

            
            _gameStateMachine.Enter<LoadLevelState, string>(SceneName.Gameplay.ToString());
        }

        public void Exit()
        {
            
        }
    }
}