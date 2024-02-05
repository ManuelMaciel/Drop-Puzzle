using Code.Runtime.Interactors;
using Code.Runtime.Logic;
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
        [SerializeField] private TextMeshProUGUI _nextShapeSize;
        [SerializeField] private GameObject _gameplayContent;
        [SerializeField] private GameObject _loseContent;
        
        private IShapeDeterminantor _shapeDeterminantor;
        private IInteractorContainer _interactorContainer;
        private ScoreInteractor _scoreInteractor;
        private MoneyInteractor _moneyInteractor;
        private Spawner _spawner;

        [Inject]
        void Construct(IPersistentProgressService persistentProgressService, IShapeDeterminantor shapeDeterminantor)
        {
            _interactorContainer = persistentProgressService.InteractorContainer;
            _shapeDeterminantor = shapeDeterminantor;
        }

        private void Start()
        {
            InitializeScoreInteractor();
            InitializeMoneyInteractor();
            InitializeTextNextShape();
        }
        
        private void OnDestroy()
        {
            _shapeDeterminantor.OnShapeChanged -= UpdateTextNextShape;
            _scoreInteractor.OnScoreIncreased -= UpdateScoreText;
            _moneyInteractor.OnCollectCoins -= UpdateCoinsText;
        }

        public void ChangeToLose()
        {
            _gameplayContent.SetActive(false);
            _loseContent.SetActive(true);
        }

        private void InitializeTextNextShape()
        {
            _shapeDeterminantor.OnShapeChanged += UpdateTextNextShape;

            UpdateTextNextShape();
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

        private void UpdateTextNextShape()
        {
            _nextShapeSize.text = _shapeDeterminantor.NextShapeSize.ToString();
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
    }
}