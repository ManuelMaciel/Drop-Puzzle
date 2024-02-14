using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Interactors;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class RestartGameWindow : WindowBase
    {
        [SerializeField] private Button restartButton;
        
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        protected override void Initialize()
        {
            
        }

        protected override void SubscribeUpdates()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        protected override void Cleanup()
        {
            restartButton.onClick.RemoveListener(RestartGame);
        }

        private void RestartGame()
        {
            _interactorContainer.Get<GameplayShapesInteractor>().ClearShapesData();
            _interactorContainer.Get<ScoreInteractor>().ResetCurrentScore();

            _windowService.Close();
                
            _gameStateMachine.Enter<LoadSceneState, string>(SceneName.Gameplay.ToString());
        }
    }
}