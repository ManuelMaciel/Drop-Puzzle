using System;
using Code.Runtime.Configs;
using Code.Runtime.Services.AudioService;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class PlaySfxButton : MonoBehaviour
    {
        [SerializeField] private SfxType sfxType;
        
        private Button _button;
        private Vector3 _startScale;
        private Tween _currentTween;
        
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService) =>
            _audioService = audioService;

        private void Awake()
        {
            _button = this.GetComponent<Button>();
            _startScale = this.transform.localScale;
        }

        private void OnEnable() =>
            _button.onClick.AddListener(Play);

        private void OnDisable() =>
            _button.onClick.RemoveListener(Play);

        private void OnDestroy() =>
            _currentTween?.Kill();

        private void Play()
        {
            _currentTween = this.transform.DOPunchScale(_startScale * 0.2f, .25f);
            
            PlaySfx();
        }

        private void PlaySfx() =>
            _audioService.PlaySfx(sfxType);
    }
}