using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class SettingsInteractor : Interactor<SettingsRepository>
    {
        public bool IsVibrate() =>
            _repository.IsVibrate;

        public void SetVibration(bool isEnable) =>
            _repository.IsVibrate = isEnable;
    }
}