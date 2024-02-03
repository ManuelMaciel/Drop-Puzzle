using Code.Runtime.Interactors;
using Code.Runtime.Repositories;

namespace Code.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        void Initialize(PlayerProgress playerProgress);

        void SaveProgress();
        bool TryLoadProgress(out PlayerProgress playerProgress);
        
        void AddUpdatebleProgress(IUpdatebleProgress updatebleProgress);
        void RemoveUpdatebleProgress(IUpdatebleProgress updatebleProgress);
        void ClearUpdatebleProgress();
    }
}