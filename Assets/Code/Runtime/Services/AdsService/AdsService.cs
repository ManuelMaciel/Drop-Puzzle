using System;
using Code.Runtime.Services.LogService;
using UnityEngine.Advertisements;
using Zenject;

namespace Code.Runtime.Services.AdsService
{
    public class AdsService : IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private readonly ILogService _logService;
        public event Action RewardedVideoReady;
        public bool IsRewardedVideoReady => _rewardedVideoReady;

        private const string AndroidGameId = "5522750";
        private const string IOSGameId = "5522751";
        private const bool TestMode = true;

        private const string UnityRewardedVideoIdAndroid = "Rewarded_Android";
        private const string UnityRewardedVideoIdIOS = "Rewarded_iOS";

        private string _gameId;
        private string _adUnitId;

        private Action _onVideoFinished;
        private bool _rewardedVideoReady;

        public AdsService(ILogService logService)
        {
            _logService = logService;
            
            InitializeAds();
        }

        private void InitializeRewardAd()
        {
#if UNITY_IOS
            _adUnitId = UnityRewardedVideoIdIOS;
#elif UNITY_ANDROID
            _adUnitId = UnityRewardedVideoIdAndroid;
#endif

            Advertisement.Load(_adUnitId, this);
        }

        public void InitializeAds()
        {
#if UNITY_IOS
            _gameId = IOSGameId;
#elif UNITY_ANDROID
            _gameId = AndroidGameId;
#elif UNITY_EDITOR
            _gameId = AndroidGameId;
#endif

            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, TestMode, this);
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