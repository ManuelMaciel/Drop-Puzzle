using Code.Runtime.Repositories;

namespace Code.Services.Progress
{
    public interface IPersistentProgressService
    {
        IInteractorContainer InteractorContainer { get; set; }
    }
}