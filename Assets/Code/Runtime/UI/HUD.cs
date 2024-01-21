using Code.Runtime.Interactors;
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
        [SerializeField] private TextMeshProUGUI _maxScoreText;
        
        private IInteractorContainer _interactorContainer;
        private ScoreInteractor _scoreInteractor;

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
            _scoreInteractor = _interactorContainer.Get<ScoreInteractor>();
            
            _scoreInteractor.OnScoreIncreased += UpdateScoreText;
            
            UpdateScoreText(_scoreInteractor.GetCurrentScore());
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = score.ToString();
            _maxScoreText.text = _scoreInteractor.GetMaxScore().ToString();
        }
        
        public class Factory : PlaceholderFactory<HUD>
        {
        }
    }
}