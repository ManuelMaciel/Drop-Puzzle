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

        public float moveDuration = 1f;
        public float scaleDuration = 0.5f;
        public float delayBeforeShrink = 0.5f;
        
        private IPersistentProgressService _progressService;
        private ShapeInteractor _shapeInteractor;
        private ScoreInteractor _scoreInteractor;

        [Inject]
        public void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;

            _shapeInteractor = _progressService.InteractorContainer.Get<ShapeInteractor>();
            _scoreInteractor = _progressService.InteractorContainer.Get<ScoreInteractor>();
        }

        private void OnEnable()
        {
            _shapeInteractor.OnShapeCombined += OnShapeCombined;
        }

        private void OnDisable()
        {
            _shapeInteractor.OnShapeCombined -= OnShapeCombined;
        }

        private void OnShapeCombined(IShapeBase shape)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(shape.transform.position);
            TextMeshProUGUI scoreText = Instantiate(scoreTextPrefab, screenPos, Quaternion.identity, this.transform);

            scoreText.rectTransform.DOMoveY(scoreText.rectTransform.position.y + 100f, moveDuration).SetEase(Ease.OutQuad);
            
            // Увеличение масштаба текста
            scoreText.rectTransform.DOScale(1.5f, scaleDuration);

            // Плавное исчезновение и уменьшение текста
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delayBeforeShrink)
                .Append(scoreText.rectTransform.DOScale(0f, scaleDuration))
                .OnComplete(() => Destroy(scoreText.gameObject));
            
            // scoreText.rectTransform.anchoredPosition = screenPos;
            scoreText.text = $"+{_scoreInteractor.GetScoreByShapeSize(shape.ShapeSize).ToString()}";
        }
    }
}