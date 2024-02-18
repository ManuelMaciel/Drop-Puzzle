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
        public float fadeDuration = 1f;

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

            scoreText.rectTransform.DOMoveY(scoreText.rectTransform.position.y + 100f, moveDuration)
                .SetEase(Ease.OutQuad).OnComplete(() => Destroy(scoreText));
            
            // scoreText.rectTransform.anchoredPosition = screenPos;
            scoreText.text = $"+{_scoreInteractor.GetScoreByShapeSize(shape.ShapeSize).ToString()}";
        }
    }
}