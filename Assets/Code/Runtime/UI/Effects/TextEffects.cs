using Code.Runtime.Infrastructure.ObjectPool;
using Code.Runtime.Interactors;
using Code.Runtime.Logic.Gameplay;
using Code.Runtime.Services.Progress;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI.Effects
{
    public class TextEffects : MonoBehaviour
    {
        private const int TextPreloadCount = 10;

        [SerializeField] private TextMeshProUGUI textPrefab;
        [SerializeField] private CoinsEffect coinsEffect;

        [Header("Animation Settings")]
        [SerializeField] private float moveScoreDuration = 1f;
        [SerializeField] private float scaleDuration = 0.5f;
        [SerializeField] private float scaleScoreAnimation = 1.5f;
        [SerializeField] private float scaleComboAnimation = 2f;
        [SerializeField] private float comboTextRotateOffset = 10f;
        [SerializeField] private int comboRotationLoopCount = 3;
        [SerializeField] private float comboTextRotateDuration = 0.2f;
        [SerializeField] private float comboTextBorderOffset = 250f;
        [SerializeField] private float scoreTextAnimationYOffset = 100f;

        private IPersistentProgressService _progressService;
        private IComboDetector _comboDetector;
        private IGameObjectPool<TextMeshProUGUI> _textPool;
        private IGameObjectsPoolContainer _gameObjectsPoolContainer;

        private ScoreInteractor _scoreInteractor;
        private ShapeInteractor _shapeInteractor;

        [Inject]
        public void Construct(IPersistentProgressService progressService, IComboDetector comboDetector,
            IGameObjectsPoolContainer gameObjectsPoolContainer)
        {
            _gameObjectsPoolContainer = gameObjectsPoolContainer;
            _comboDetector = comboDetector;
            _progressService = progressService;
        }

        private void Awake()
        {
            _shapeInteractor = _progressService.InteractorContainer.Get<ShapeInteractor>();
            _scoreInteractor = _progressService.InteractorContainer.Get<ScoreInteractor>();

            _textPool = new ComponentPool<TextMeshProUGUI>(textPrefab, TextPreloadCount, _gameObjectsPoolContainer);
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
            TextMeshProUGUI scoreText = GetText(screenPos);

            ScoreAnimation(scoreText);

            scoreText.text = $"+{_scoreInteractor.GetScoreByShapeSize(shape.ShapeSize)}";
        }

        private void OnComboDetected(int comboCount, Vector3 comboPosition)
        {
            Vector3 screenPos = GetComboTextPosition(comboPosition);
            TextMeshProUGUI comboText = GetText(screenPos);

            ComboAnimation(comboText);

            comboText.color = Color.yellow;
            comboText.text = $"Combo {comboCount}X";
            coinsEffect.AddCoins(screenPos, comboCount);
        }

        private Vector3 GetComboTextPosition(Vector3 comboPosition)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(comboPosition);
            screenPos.x = Mathf.Clamp(screenPos.x, comboTextBorderOffset, Screen.width - comboTextBorderOffset);

            return screenPos;
        }

        private TextMeshProUGUI GetText(Vector3 screenPos)
        {
            TextMeshProUGUI scoreText = _textPool.Get(screenPos);
            scoreText.rectTransform.SetParent(this.transform);

            return scoreText;
        }

        private void ClearText(TextMeshProUGUI comboText)
        {
            comboText.color = Color.white;
            comboText.rectTransform.rotation = Quaternion.identity;
            comboText.rectTransform.localScale = Vector3.one;
            _textPool.Return(comboText);
        }

        private void ScoreAnimation(TextMeshProUGUI scoreText)
        {
            scoreText.rectTransform.DOMoveY(scoreText.rectTransform.position.y + scoreTextAnimationYOffset,
                    moveScoreDuration)
                .SetEase(Ease.OutQuad);

            Sequence sequence = DOTween.Sequence()
                .Append(scoreText.rectTransform.DOScale(scaleScoreAnimation, scaleDuration))
                .Append(scoreText.rectTransform.DOScale(0f, scaleDuration))
                .OnComplete(() => ClearText(scoreText));
        }

        private void ComboAnimation(TextMeshProUGUI comboText)
        {
            comboText.rectTransform.DOScale(scaleComboAnimation, scaleDuration);

            Sequence sequence = DOTween.Sequence();

            for (int i = 0; i < comboRotationLoopCount; i++)
            {
                sequence.Append(comboText.rectTransform.DORotate(new Vector3(0, 0, comboTextRotateOffset), comboTextRotateDuration));
                sequence.Append(comboText.rectTransform.DORotate(new Vector3(0, 0, -comboTextRotateOffset), comboTextRotateDuration));
            }

            sequence
                .Append(comboText.rectTransform.DOScale(0, 0.5f))
                .OnComplete(() => ClearText(comboText));
        }
    }
}