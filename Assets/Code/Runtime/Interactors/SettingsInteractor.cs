using System;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class SettingsInteractor : Interactor<SettingsRepository>
    {
        public event Action<bool> OnChangeAmbientMode;
        
        public bool IsEnableVibrate() =>
            _repository.IsEnableVibrate;

        public void SetEnableVibration(bool isEnable) =>
            _repository.IsEnableVibrate = isEnable;

        public bool IsEnableSFX() =>
            _repository.IsEnableSFX;
        
        public void SetEnableSFX(bool isEnable) =>
            _repository.IsEnableSFX = isEnable;
        
        public bool IsEnableAmbient() =>
            _repository.IsEnableAmbient;

        public void SetEnableAmbient(bool isEnableAmbient)
        {
            _repository.IsEnableAmbient = isEnableAmbient;
            
            OnChangeAmbientMode?.Invoke(isEnableAmbient);
        }
    }
}