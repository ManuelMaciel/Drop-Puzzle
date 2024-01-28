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
        [SerializeField] private TextMeshProUGUI _coinsText;

        private IInteractorContainer _interactorContainer;
        private ScoreInteractor _scoreInteractor;
        private MoneyInteractor _moneyInteractor;

        [Inject]
        void Construct(IPersistentProgressService persistentProgressService)
        {
            _interactorContainer = persistentProgressService.InteractorContainer;
        }

        private void Start()
        {
            InitializeScoreInteractor();
            InitializeMoneyInteractor();
        }

        private void InitializeScoreInteractor()
        {
            _scoreInteractor = _interactorContainer.Get<ScoreInteractor>();

            _scoreInteractor.OnScoreIncreased += UpdateScoreText;

            UpdateScoreText(_scoreInteractor.GetCurrentScore());
        }

        private void InitializeMoneyInteractor()
        {
            _moneyInteractor = _interactorContainer.Get<MoneyInteractor>();

            _moneyInteractor.OnCollectCoins += UpdateCoinsText;

            UpdateCoinsText(_moneyInteractor.GetCoins());
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = score.ToString();
            _maxScoreText.text = _scoreInteractor.GetMaxScore().ToString();
        }

        private void UpdateCoinsText(int coins)
        {
            _coinsText.text = coins.ToString();
        }

        public class Factory : PlaceholderFactory<string, HUD>
        {
        }
    }
}