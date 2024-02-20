using System;
using Code.Runtime.Interactors;
using Code.Runtime.Logic;
using Code.Runtime.Repositories;
using Code.Runtime.UI.Effects;
using Code.Services.StaticDataService;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    [Serializable]
    public class HUDGameplayContent : IDisposable
    {
        private const float AnimationScaleFactor = 0.2f;
        private const float AnimationScaleDuration = 0.2f;
        
        [SerializeField] private Image _nextShapeImage;
        [SerializeField] private RectTransform _coinTransform;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _maxScoreText;
        [SerializeField] private TextMeshProUGUI _coinsText;

        private IShapeDeterminantor _shapeDeterminantor;
        private IInteractorContainer _interactorContainer;
        private IStaticDataService _staticDataService;
        
        private ScoreInteractor _scoreInteractor;
        private MoneyInteractor _moneyInteractor;
        private CoinsEffect _coinsEffect;
        private Vector3 _coinScale;
        private Vector3 _nextCoinScale;

        public void Construct(IShapeDeterminantor shapeDeterminantor, IInteractorContainer interactorContainer,
            IStaticDataService staticDataService, CoinsEffect coinsEffect)
        {
            _coinsEffect = coinsEffect;
            _shapeDeterminantor = shapeDeterminantor;
            _interactorContainer = interactorContainer;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            InitializeScoreInteractor();
            InitializeMoneyInteractor();
            InitializeTextNextShape();

            _coinScale = _coinTransform.lossyScale;
            _nextCoinScale = _nextShapeImage.rectTransform.localScale;
        }

        public void Dispose()
        {
            _shapeDeterminantor.OnShapeChanged -= UpdateImageNextShape;
            _scoreInteractor.OnScoreIncreased -= UpdateScoreText;
            _coinsEffect.OnCoinAdded -= UpdateCoinsText;
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

            _coinsEffect.OnCoinAdded += UpdateCoinsText;

            UpdateCoinsText();
        }

        private void UpdateImageNextShape()
        {
            _nextShapeImage.sprite =
                _staticDataService.ShapeSizeConfig.Sprites[(int)_shapeDeterminantor.NextShapeSize];
            _nextShapeImage.rectTransform.DOPunchScale(_nextCoinScale * AnimationScaleFactor, AnimationScaleDuration);
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = score.ToString();
            _maxScoreText.text = _scoreInteractor.GetMaxScore().ToString();
        }

        private void UpdateCoinsText()
        {
            _coinsText.text = _moneyInteractor.GetCoins().ToString();
            _coinTransform.DOPunchScale(_coinScale * AnimationScaleFactor, AnimationScaleDuration);
        }
    }
}