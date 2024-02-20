using Code.Runtime.Infrastructure.ObjectPool;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Runtime.UI
{
    public class CoinsEffect : MonoBehaviour
    {
        private const int PreloadCoinsCount = 10;

        [SerializeField] private RectTransform coinPrefab;
        [SerializeField] private RectTransform coinWalletField;

        [Header("Animation Settings")]
        [SerializeField] [Range(0.5f, 0.9f)] private float minAnimDuration = 0.7f;
        [SerializeField] [Range(0.9f, 2f)] private float maxAnimDuration = 1.8f;
        [SerializeField] private Ease easeType = Ease.InBounce;
        [SerializeField] private float spread = 0.5f;
        [SerializeField] private float scaleDuration = 0.5f;

        private Vector3 _targetPosition;
        private IGameObjectsPoolContainer _gameObjectsPoolContainer;
        private IObjectPool<RectTransform> _coinsPool;

        [Inject]
        public void Construct(IGameObjectsPoolContainer gameObjectsPoolContainer)
        {
            _gameObjectsPoolContainer = gameObjectsPoolContainer;
        }

        private void Start()
        {
            _targetPosition = coinWalletField.position;
            _coinsPool = new ComponentPool<RectTransform>(coinPrefab, PreloadCoinsCount, _gameObjectsPoolContainer);
            _coinsPool.Initialize();
        }

        public void AddCoins(Vector3 collectedCoinPosition, int amount) =>
            Animate(collectedCoinPosition, amount);

        private void Animate(Vector3 collectedCoinPosition, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                RectTransform coinTransform = GetCoinTransform();
                float duration = Random.Range(minAnimDuration, maxAnimDuration);

                coinTransform.position = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);

                CoinAnimation(coinTransform, duration);
            }
        }

        private void CoinAnimation(RectTransform coinTransform, float duration)
        {
            DOTween.Sequence()
                .Append(coinTransform.DOMove(_targetPosition, duration).SetEase(easeType))
                .Insert(duration - scaleDuration, coinTransform.DOScale(Vector3.zero, scaleDuration))
                .OnComplete(() => ClearCoinTransform(coinTransform));
        }

        private RectTransform GetCoinTransform()
        {
            RectTransform coinTransform = _coinsPool.Get();
            coinTransform.SetParent(this.transform);

            return coinTransform;
        }

        private void ClearCoinTransform(RectTransform coinTransform)
        {
            coinTransform.localScale = Vector3.one;
            _coinsPool.Return(coinTransform);
        }
    }
}