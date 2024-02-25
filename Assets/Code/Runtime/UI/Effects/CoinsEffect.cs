using System;
using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.ObjectPool;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Runtime.UI.Effects
{
    public class CoinsEffect : MonoBehaviour
    {
        public enum AnimationType
        {
            Spot,
            Splash,
        }

        private const float TimeFactor = 0.025f;

        public event Action OnCoinAdded;

        [SerializeField] private RectTransform coinPrefab;
        [SerializeField] private RectTransform coinsField;

        [Header("Animation Settings")] [SerializeField] [Range(0.5f, 0.9f)]
        private float minMoveAnimDuration = 0.7f;

        [SerializeField] [Range(0.9f, 2f)] private float maxMoveAnimDuration = 1.8f;
        [SerializeField] private Ease easeType = Ease.InBounce;
        [SerializeField] private float spread = 0.5f;
        [SerializeField] private float splashSpread = 150f;
        [SerializeField] private float scaleDuration = 0.5f;
        [SerializeField] private float scaleSplashDuration = 0.3f;
        [SerializeField] private int maxOffsetTimeSplash = 15;

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
            _targetPosition = coinsField.position;
            _coinsPool = new ComponentPool<RectTransform>(coinPrefab, ObjectPoolStaticData.PreloadCoinsCount, _gameObjectsPoolContainer);
            _coinsPool.Initialize();
        }

        public void AddCoins(Vector3 collectedCoinPosition, int amount,
            AnimationType animationType = AnimationType.Spot) =>
            Animate(collectedCoinPosition, amount, animationType);

        private void Animate(Vector3 collectedCoinPosition, int amount, AnimationType animationType)
        {
            Sequence coinAnimation = DOTween.Sequence();

            for (int i = 0; i < amount; i++)
            {
                RectTransform coinTransform = GetCoinTransform();
                float duration = Random.Range(minMoveAnimDuration, maxMoveAnimDuration);
                float range = animationType == AnimationType.Spot ? spread : splashSpread;
                float randomPositionX = Random.Range(-range, range);
                float randomPositionY = Random.Range(-range, range);

                coinTransform.position = collectedCoinPosition + new Vector3(randomPositionX, randomPositionY, 0f);

                switch (animationType)
                {
                    case AnimationType.Spot:
                        SpotAnimation(coinAnimation, coinTransform, duration);
                        break;
                    case AnimationType.Splash:
                        SplashAnimation(coinAnimation, coinTransform, duration);
                        break;
                }
            }

            coinAnimation.OnComplete(() => OnCoinAdded?.Invoke());
        }

        private void SpotAnimation(Sequence coinAnimation, RectTransform coinTransform, float duration)
        {
            coinAnimation.Insert(0f, CoinAnimation(coinTransform, duration));
        }

        private void SplashAnimation(Sequence coinAnimation, RectTransform coinTransform, float duration)
        {
            coinTransform.localScale = Vector3.zero;
            
            float t = Random.Range(0, maxOffsetTimeSplash) * TimeFactor;
            coinAnimation.Insert(t, coinTransform.DOScale(Vector3.one, scaleSplashDuration + t));
            coinAnimation.Insert(scaleSplashDuration + t, CoinAnimation(coinTransform, duration));
        }

        private Sequence CoinAnimation(RectTransform coinTransform, float duration)
        {
            return DOTween.Sequence()
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