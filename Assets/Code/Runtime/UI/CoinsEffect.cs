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

        [Space] [Header("Animation settings")] [SerializeField] [Range(0.5f, 0.9f)]
        float minAnimDuration;

        [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;
        [SerializeField] Ease easeType;
        [SerializeField] float spread;

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
                RectTransform coinTransform = _coinsPool.Get();
                coinTransform.SetParent(this.transform);

                coinTransform.position = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);

                float duration = Random.Range(minAnimDuration, maxAnimDuration);

                Sequence sequence = DOTween.Sequence();
                sequence.Append(coinTransform.DOMove(_targetPosition, duration).SetEase(easeType));
                sequence.Insert(duration - 0.5f, coinTransform.DOScale(Vector3.zero, 0.5f));
                sequence.OnComplete(() => _coinsPool.Return(coinTransform));
            }
        }
    }
}