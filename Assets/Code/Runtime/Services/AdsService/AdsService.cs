using System;
using Code.Runtime.Configs;
using Code.Runtime.Services.LogService;
using Code.Runtime.Services.StaticDataService;
using UnityEngine.Advertisements;

namespace Code.Runtime.Services.AdsService
{
    public class AdsService : IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public event Action RewardedVideoReady;
        public bool IsRewardedVideoReady => _rewardedVideoReady;

        private readonly ILogService _logService;
        private readonly IStaticDataService _staticDataService;

        private Action _onVideoFinished;
        private AdConfig _adConfig;
        private bool _rewardedVideoReady;
        private string _gameId;
        private string _adUnitId;

        public AdsService(ILogService logService, IStaticDataService staticDataService)
        {
            _logService = logService;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            _adConfig = _staticDataService.AdConfig;
            
            InitializeAds();
        }

        private void InitializeRewardAd()
        {
#if UNITY_IOS
            _adUnitId = _adConfig.UnityRewardedVideoIdIOS;
#elif UNITY_ANDROID
            _adUnitId = _adConfig.UnityRewardedVideoIdAndroid;
#endif

            Advertisement.Load(_adUnitId, this);
        }

        public void InitializeAds()
        {
#if UNITY_IOS
            _gameId = _adConfig.IOSGameId;
#elif UNITY_ANDROID
            _gameId = _adConfig.AndroidGameId;
#elif UNITY_EDITOR
            _gameId = _adConfig.AndroidGameId;
#endif

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _adConfig.TestMode, this);
            }
        }

        public void OnInitializationComplete()
        {
            InitializeRewardAd();
            
            _logService.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message) =>
            _logService.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            _onVideoFinished = onVideoFinished;
            Advertisement.Show(_adUnitId, this);
        }

        // If the ad successfully loads, add a listener to the button and enable it:
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            _logService.Log("Ad Loaded: " + adUnitId);

            if (adUnitId.Equals(_adUnitId))
            {
                RewardedVideoReady?.Invoke();

                _rewardedVideoReady = true;
            }
        }

        // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                _onVideoFinished?.Invoke();
                // Grant a reward.
            }
        }

        // Implement Load and Show Listener error callbacks:
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            _logService.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            _logService.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }
    }
}