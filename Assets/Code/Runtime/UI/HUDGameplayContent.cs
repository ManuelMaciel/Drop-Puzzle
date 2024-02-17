using System;
using Code.Runtime.Interactors;
using Code.Runtime.Logic;
using Code.Runtime.Repositories;
using Code.Services.StaticDataService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    [Serializable]
    public class HUDGameplayContent : IDisposable
    {
        [SerializeField] private Image _nextShapeImage;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _maxScoreText;
        [SerializeField] private TextMeshProUGUI _coinsText;

        private IShapeDeterminantor _shapeDeterminantor;
        private IInteractorContainer _interactorContainer;
        private IStaticDataService _staticDataService;
        
        private ScoreInteractor _scoreInteractor;
        private MoneyInteractor _moneyInteractor;

        public void Construct(IShapeDeterminantor shapeDeterminantor, IInteractorContainer interactorContainer,
            IStaticDataService staticDataService)
        {
            _shapeDeterminantor = shapeDeterminantor;
            _interactorContainer = interactorContainer;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            InitializeScoreInteractor();
            InitializeMoneyInteractor();
            InitializeTextNextShape();
        }

        public void Dispose()
        {
            _shapeDeterminantor.OnShapeChanged -= UpdateImageNextShape;
            _scoreInteractor.OnScoreIncreased -= UpdateScoreText;
            _moneyInteractor.OnCollectCoins -= UpdateCoinsText;
        }

        private void InitializeTextNextShape()
        {
            _shapeDeterminantor.OnShapeChanged += UpdateImageNextShape;

            UpdateImageNextShape();
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

        private void UpdateImageNextShape()
        {
            _nextShapeImage.sprite =
                _staticDataService.ShapeSizeConfig.Sprites[(int)_shapeDeterminantor.NextShapeSize];
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