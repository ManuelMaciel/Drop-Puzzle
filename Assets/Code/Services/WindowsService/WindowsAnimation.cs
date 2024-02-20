using System;
using DG.Tweening;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Code.Services.WindowsService
{
    public class WindowsAnimation : IWindowsAnimation
    {
        private const float AnimationDuration = 0.3f;
        private const float BackgroundOpenTransparency = 0.4f;
        private const float WindowScale = 1f;

        private Image _backgroundImage;

        public WindowsAnimation(Transform windowRoot)
        {
            _backgroundImage = windowRoot.GetComponentInChildren<Image>();

            if (_backgroundImage == null) Debug.LogError("Windows root don't have background image");
        }

        public void OpenAnimation(Transform window)
        {
            window.localScale = Vector3.zero;
            _backgroundImage.enabled = true;
            
            DOTween.Sequence()
                .Append(window.DOScale(WindowScale, AnimationDuration).SetEase(Ease.InOutBack))
                .Insert(0f, _backgroundImage.DOFade(BackgroundOpenTransparency, AnimationDuration));
        }

        public void CloseAnimation(Transform window, Action onCallback)
        {
            DOTween.Sequence()
                .Append(window.DOScale(0f, AnimationDuration).SetEase(Ease.InBack))
                .Insert(0f, _backgroundImage.DOFade(0f, AnimationDuration))
                .OnComplete(() =>
                {
                    onCallback?.Invoke();
                    _backgroundImage.enabled = false;
                });
        }
    }
}