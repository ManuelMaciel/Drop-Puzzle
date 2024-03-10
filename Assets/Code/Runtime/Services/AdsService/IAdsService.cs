using System;

namespace Code.Runtime.Services.AdsService
{
    public interface IAdsService
    {
        event Action RewardedVideoReady;
        bool IsRewardedVideoReady { get; }
        void ShowRewardedVideo(Action onVideoFinished);
        void Initialize();
    }
}