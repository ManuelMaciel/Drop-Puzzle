using Code.Runtime.Interactors;
using Code.Runtime.Services.AdsService;
using Code.Runtime.Services.Progress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class AdRewardButton : MonoBehaviour
    {
        private Button _button;
        
        private IAdsService _adsService;
        private IPersistentProgressService _progressService;

        [Inject]
        public void Construct(IAdsService adsService, IPersistentProgressService progressService)
        {
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
            _progressService.InteractorContainer.Get<MoneyInteractor>().AddCoins(999);
        }
    }
}