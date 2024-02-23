using Code.Runtime.Interactors;
using Code.Runtime.Services.SaveLoadService;
using Code.Runtime.UI.Buttons;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private SwitchButton switchVibrationButton;
        [SerializeField] private SwitchButton switchSFXButton;
        [SerializeField] private SwitchButton switchAmbientButton;
        
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

            switchVibrationButton.Initialize();
            switchAmbientButton.Initialize();
            switchSFXButton.Initialize();
            
            UpdateButtonState(_settingsInteractor.IsEnableVibrate(), switchVibrationButton);
            UpdateButtonState(_settingsInteractor.IsEnableSFX(), switchSFXButton);
            UpdateButtonState(_settingsInteractor.IsEnableAmbient(), switchAmbientButton);
        }

        protected override void SubscribeUpdates()
        {
            switchVibrationButton.OnStateChanged += SwitchVibrationMode;
            switchSFXButton.OnStateChanged += SwitchSFXMode;
            switchAmbientButton.OnStateChanged += SwitchAmbientMode;
        }

        protected override void Cleanup()
        {
            switchVibrationButton.OnStateChanged += SwitchVibrationMode;
            switchSFXButton.OnStateChanged += SwitchSFXMode;
            switchAmbientButton.OnStateChanged += SwitchAmbientMode;
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

        private void UpdateButtonState(bool isEnable, SwitchButton button)
        {
            button.UpdateState(isEnable);

            _saveLoadService.SaveProgress();
        }
    }
}