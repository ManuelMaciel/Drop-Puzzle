using Code.Runtime.Interactors;
using Code.Runtime.Logic.Gameplay;
using Code.Services.Progress;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI
{
    public class ScoreAddedEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextPrefab;
        [SerializeField] private CoinsEffect coinsEffect;

        public float moveDuration = 1f;
        public float scaleDuration = 0.5f;
        public float delayBeforeShrink = 0.5f;

        private IPersistentProgressService _progressService;
        private ShapeInteractor _shapeInteractor;
        private ScoreInteractor _scoreInteractor;
        private IComboDetector _comboDetector;

        [Inject]
        public void Construct(IPersistentProgressService progressService, IComboDetector comboDetector)
        {
            _comboDetector = comboDetector;
            _progressService = progressService;

            _shapeInteractor = _progressService.InteractorContainer.Get<ShapeInteractor>();
            _scoreInteractor = _progressService.InteractorContainer.Get<ScoreInteractor>();
        }

        private void OnEnable()
        {
            _shapeInteractor.OnShapeCombined += OnShapeCombined;
            _comboDetector.OnComboDetected += OnComboDetected;
        }

        private void OnDisable()
        {
            _shapeInteractor.OnShapeCombined -= OnShapeCombined;
            _comboDetector.OnComboDetected -= OnComboDetected;
        }

        private void OnShapeCombined(IShapeBase shape)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(shape.transform.position);
            TextMeshProUGUI scoreText = Instantiate(scoreTextPrefab, screenPos, Quaternion.identity, this.transform);

            scoreText.rectTransform.DOMoveY(scoreText.rectTransform.position.y + 100f, moveDuration)
                .SetEase(Ease.OutQuad);
            
            scoreText.rectTransform.DOScale(1.5f, scaleDuration);
            
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delayBeforeShrink)
                .Append(scoreText.rectTransform.DOScale(0f, scaleDuration))
                .OnComplete(() => Destroy(scoreText.gameObject));
            
            scoreText.text = $"+{_scoreInteractor.GetScoreByShapeSize(shape.ShapeSize)}";
        }

        private void OnComboDetected(int comboCount, Vector3 comboPosition)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(comboPosition);
            TextMeshProUGUI comboText = Instantiate(scoreTextPrefab, screenPos, Quaternion.identity, this.transform);
            
            comboText.rectTransform.DOScale(2f, scaleDuration);
            
            Sequence sequence = DOTween.Sequence();
            
            for (int i = 0; i < 3; i++)
            {
                sequence.Append(comboText.rectTransform.DORotate(new Vector3(0, 0, 10), 0.2f));
                sequence.Append(comboText.rectTransform.DORotate(new Vector3(0, 0, -10), 0.2f));
            }
            
            sequence
                .Append(comboText.rectTransform.DOScale(0, 0.5f))
                .OnComplete(() => Destroy(comboText.gameObject));

            comboText.color = Color.yellow;
            comboText.text = $"Combo {comboCount}X";
            coinsEffect.AddCoins(screenPos, comboCount);
        }
    }
}