using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Runtime.Services.Progress;
using Code.Runtime.Services.StaticDataService;
using Plugin.DocuFlow.Documentation;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Services.AudioService
{
    [Doc("The AudioService class manages audio playback within the game, including ambient sound, sound effects (SFX), and device vibrations. It utilizes Unity's AudioSource component for playing audio clips and interacts with static data configurations and player settings to control audio behavior.")]
    [RequireComponent(typeof(AudioSource))]
    public class AudioService : MonoBehaviour, IAudioService
    {
        private AudioSource _audioSource;

        private IStaticDataService _staticDataService;
        private SettingsInteractor _settingsInteractor;
        private IPersistentProgressService _persistentProgressService;

        [Inject]
        public void Construct(IStaticDataService staticDataService,
            IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
            _staticDataService = staticDataService;
        }

        private void Awake() =>
            _audioSource = this.GetComponent<AudioSource>();

        public void OnDestroy() =>
            _settingsInteractor.OnChangeAmbientMode -= OnChangeAmbientMode;

        public void Initialize()
        {
            _settingsInteractor = _persistentProgressService.InteractorContainer.Get<SettingsInteractor>();
            _audioSource.clip = _staticDataService.AudioConfig.Ambient;
            
            _settingsInteractor.OnChangeAmbientMode += OnChangeAmbientMode;
            
            PlayAmbient();
        }

        public void PlayAmbient()
        {
            if (!_settingsInteractor.IsEnableAmbient()) return; 
            
            _audioSource.Play();
        }

        public void PlayVibrate()
        {
            if (!_settingsInteractor.IsEnableVibrate()) return;

#if !UNITY_WEBGL
            Handheld.Vibrate();
#endif
        }

        public void PlaySfx(SfxType sfxType)
        {
            if (!_settingsInteractor.IsEnableSFX()) return;

            SfxData sfxData = _staticDataService.AudioConfig.SfxsData.Find(sd => sd.SfxType == sfxType);

            _audioSource.PlayOneShot(sfxData.Sfx, sfxData.Volume);
        }

        private void OnChangeAmbientMode(bool isEnable)
        {
            if (isEnable)
                _audioSource.Play();
            else
                _audioSource.Stop();
        }
    }
}