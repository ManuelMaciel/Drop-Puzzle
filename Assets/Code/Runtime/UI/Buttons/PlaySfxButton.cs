﻿using Code.Runtime.Configs;
using Code.Services.AudioService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI
{
    [RequireComponent(typeof(Button))]
    public class PlaySfxButton : MonoBehaviour
    {
        [SerializeField] private SfxType sfxType;
        
        private Button _button;
        
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService) =>
            _audioService = audioService;

        private void Awake() =>
            _button = this.GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(PlaySfx);

        private void OnDisable() =>
            _button.onClick.RemoveListener(PlaySfx);

        private void PlaySfx() =>
            _audioService.PlaySfx(sfxType);
    }
}