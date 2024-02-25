using System;
using Code.Runtime.Interactors;
using Code.Runtime.Logic;
using Code.Runtime.Repositories;
using Code.Runtime.Services.StaticDataService;
using Code.Runtime.UI.Effects;
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

        private IShapeDeterminantor _shapeDeterminantor;
        private IInteractorContainer _interactorContainer;
        private IStaticDataService _staticDataService;
        
        private ScoreInteractor _scoreInteractor;
        private Vector3 _nextShapeScale;

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
            InitializeTextNextShape();
            
            _nextShapeScale = _nextShapeImage.rectTransform.localScale;
        }

        public void Dispose()
        {
            _shapeDeterminantor.OnShapeChanged -= UpdateImageNextShape;
            _scoreInteractor.OnScoreIncreased -= UpdateScoreText;
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
        

        private void UpdateImageNextShape()
        {
            _nextShapeImage.sprite =
                _staticDataService.ShapeSizeConfig.Sprites[(int)_shapeDeterminantor.NextShapeSize];
            _nextShapeImage.rectTransform.DOPunchScale(_nextShapeScale * AnimationScaleFactor, AnimationScaleDuration);
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = score.ToString();
            _maxScoreText.text = _scoreInteractor.GetMaxScore().ToString();
        }
    }
}