using Code.Runtime.Interactors;
using Code.Runtime.Services.Progress;
using Code.Runtime.Services.StaticDataService;
using Code.Runtime.UI.Effects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI
{
    public class CoinsField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private RectTransform coinIconTransform;
        [SerializeField] private CoinsEffect coinsEffect;

        private IPersistentProgressService _progressService;
        private MoneyInteractor _moneyInteractor;
        private IStaticDataService _staticDataService;

        [Inject]
        public void Construct(IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
        }

        private void Start()
        {
            if (_progressService.InteractorContainer.TryGet(out _moneyInteractor))
            {
                coinsEffect.OnCoinAdded += UpdateCoinsText;

                UpdateCoinsText();
            }
        }

        private void OnDestroy()
        {
            if (_moneyInteractor != null)
                coinsEffect.OnCoinAdded -= UpdateCoinsText;
        }

        private void UpdateCoinsText()
        {
            coinsText.text = _moneyInteractor.GetCoins().ToString();
            coinIconTransform.DOPunchScale(_staticDataService.AnimationConfig.GetPunchAnimationScaleFactor(),
                _staticDataService.AnimationConfig.PunchAnimationScaleDuration);
        }
    }
}