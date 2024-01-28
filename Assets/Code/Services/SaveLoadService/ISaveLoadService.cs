using Code.Runtime.Repositories;

namespace Code.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        void Initialize(PlayerProgress playerProgress);

        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}