using Code.Runtime.Interactors;
using Code.Runtime.Services.SaveLoadService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Button switchVibrationButton;
        [SerializeField] private Button switchSFXButton;
        [SerializeField] private Button switchAmbientButton;
        
        private SettingsInteractor _settingsInteractor;
        private ISaveLoadService _saveLoadService;

        [Inject]
        public void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
        
        protected override void Initialize()
        {
            _settingsInteractor = _interactorContainer.Get<SettingsInteractor>();

            UpdateButtonState(_settingsInteractor.IsEnableVibrate(), switchVibrationButton);
            UpdateButtonState(_settingsInteractor.IsEnableSFX(), switchSFXButton);
            UpdateButtonState(_settingsInteractor.IsEnableAmbient(), switchAmbientButton);
        }

        protected override void SubscribeUpdates()
        {
            switchVibrationButton.onClick.AddListener(SwitchVibrationMode);
            switchSFXButton.onClick.AddListener(SwitchSFXMode);
            switchAmbientButton.onClick.AddListener(SwitchAmbientMode);
        }

        protected override void Cleanup()
        {
            switchVibrationButton.onClick.RemoveListener(SwitchVibrationMode);
            switchSFXButton.onClick.RemoveListener(SwitchSFXMode);
            switchAmbientButton.onClick.RemoveListener(SwitchAmbientMode);
        }

        private void SwitchAmbientMode()
        {
            bool isEnableAmbient = !_settingsInteractor.IsEnableAmbient();
            
            _settingsInteractor.SetEnableAmbient(isEnableAmbient);
            UpdateButtonState(isEnableAmbient, switchAmbientButton);
        }

        private void SwitchSFXMode()
        {
            bool isEnableSfx = !_settingsInteractor.IsEnableSFX();
            
            _settingsInteractor.SetEnableSFX(isEnableSfx);
            UpdateButtonState(isEnableSfx, switchSFXButton);
        }

        private void SwitchVibrationMode()
        {
            bool isEnableVibrate = !_settingsInteractor.IsEnableVibrate();

            _settingsInteractor.SetEnableVibration(isEnableVibrate);
            UpdateButtonState(isEnableVibrate, switchVibrationButton);
        }

        private void UpdateButtonState(bool isEnable, Button button)
        {
            button.image.color = isEnable ? Color.white : Color.gray;

            _saveLoadService.SaveProgress();
        }
    }
}