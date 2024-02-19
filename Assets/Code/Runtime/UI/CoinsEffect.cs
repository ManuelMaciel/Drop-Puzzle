using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Code.Runtime.UI
{
    public class CoinsEffect : MonoBehaviour
    {
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private RectTransform coinWalletField;

        [Space] [Header("Animation settings")] [SerializeField] [Range(0.5f, 0.9f)]
        float minAnimDuration;

        [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;
        [SerializeField] Ease easeType;
        [SerializeField] float spread;

        private Vector3 _targetPosition;

        private void Start() =>
            _targetPosition = coinWalletField.position;

        public void AddCoins(Vector3 collectedCoinPosition, int amount) =>
            Animate(collectedCoinPosition, amount);

        private void Animate(Vector3 collectedCoinPosition, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                //extract a coin from the pool
                GameObject coin = Instantiate(coinPrefab, this.transform);
                coin.SetActive(true);

                //move coin to the collected coin pos
                coin.transform.position = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);

                //animate coin to target position
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                
                // Создаем анимацию перемещения монетки к целевой позиции
                Sequence sequence = DOTween.Sequence();
                sequence.Append(coin.transform.DOMove(_targetPosition, duration)
                    .SetEase(easeType));

                // Уменьшаем размер монетки до 0 за 0.5 секунд до исчезновения
                sequence.Insert(duration - 0.5f, coin.transform.DOScale(Vector3.zero, 0.5f));

                // Выполняем действия по завершению анимации
                sequence.OnComplete(() =>
                {
                    coin.SetActive(false);
                    Destroy(coin);
                });
            }
        }
    }
}