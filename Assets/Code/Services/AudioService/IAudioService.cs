using Code.Runtime.Configs;

namespace Code.Services.AudioService
{
    public interface IAudioService
    {
        void PlayAmbient();
        void PlaySfx(SfxType sfxType);
        void PlayVibrate();
        void Initialize();
    }
}