using Code.Runtime.Repositories;
using Code.Services.Progress;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private IInteractorContainer _interactorContainer;

        [Inject]
        void Construct(IPersistentProgressService persistentProgressService)
        {
            _interactorContainer = persistentProgressService.InteractorContainer;
        }

        private void Start()
        {
            InitializeScoreInteractor();
        }

        private void InitializeScoreInteractor()
        {
            ScoreInteractor scoreInteractor = _interactorContainer.Get<ScoreInteractor>();
            
            scoreInteractor.OnScoreIncreased += UpdateScoreText;
            
            UpdateScoreText(scoreInteractor.GetCurrentScore());
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}