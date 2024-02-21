using Code.Runtime.Configs;

namespace Code.Runtime.Services.AudioService
{
    public interface IAudioService
    {
        void PlayAmbient();
        void PlaySfx(SfxType sfxType);
        void PlayVibrate();
        void Initialize();
    }
}