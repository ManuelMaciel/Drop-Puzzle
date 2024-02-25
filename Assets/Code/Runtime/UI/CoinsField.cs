using Code.Runtime.Interactors;
using Code.Runtime.Services.Progress;
using Code.Runtime.UI.Effects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI
{
    public class CoinsField : MonoBehaviour
    {
        private const float AnimationScaleFactor = 0.2f;
        private const float AnimationScaleDuration = 0.2f;
        
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private CoinsEffect coinsEffect;

        private IPersistentProgressService _progressService;
        private MoneyInteractor _moneyInteractor;
        private Vector3 _coinScale;
        
        [Inject]
        public void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
            
            _coinScale = this.transform.lossyScale;
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
            this.transform.DOPunchScale(_coinScale * AnimationScaleFactor, AnimationScaleDuration);
        }
    }
}