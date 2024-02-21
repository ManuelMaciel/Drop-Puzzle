using Code.Runtime.Repositories;

namespace Code.Runtime.Services.Progress
{
    public interface IPersistentProgressService
    {
        IInteractorContainer InteractorContainer { get; set; }
    }
}