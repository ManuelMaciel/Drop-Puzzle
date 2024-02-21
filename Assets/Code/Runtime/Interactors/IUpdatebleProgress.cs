using Code.Runtime.Services.Progress;

namespace Code.Runtime.Interactors
{
    public interface IUpdatebleProgress
    {
        void UpdateProgress(IPersistentProgressService persistentProgressService);
    }
}