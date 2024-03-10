using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Runtime.Services.AdsService;
using Code.Runtime.Services.AudioService;
using Code.Runtime.Services.Progress;
using Code.Runtime.UI.Effects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class AdRewardButton : MonoBehaviour
    {
        [FormerlySerializedAs("_coinsEffect")] 
        [SerializeField] private CoinsEffect coinsEffect;
        [SerializeField] private int animationCoinsAmount = 10;

        private Button _button;
        private IPersistentProgressService _progressService;
        private IAudioService _audioService;
        private IAdsService _adsService;

        [Inject]
        public void Construct(IAdsService adsService, IPersistentProgressService progressService, IAudioService audioService)
        {
            _audioService = audioService;
            _progressService = progressService;
            _adsService = adsService;
        }

        private void Awake() =>
            _button = this.GetComponent<Button>();

        private void OnEnable()
        {
            _button.onClick.AddListener(OnShowAd);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnShowAd);
        }

        private void OnShowAd()
        {
            if(!_adsService.IsRewardedVideoReady) return;
            
            _adsService.ShowRewardedVideo(OnVideoFinished);
        }

        private void OnVideoFinished()
        {
            _progressService.InteractorContainer.Get<MoneyInteractor>().AddReward();
            coinsEffect.AddCoins(new Vector2(Screen.width / 2f, Screen.height / 2f), animationCoinsAmount, CoinsEffect.AnimationType.Splash);
            _audioService.PlaySfx(SfxType.Reward);
        }
    }
}