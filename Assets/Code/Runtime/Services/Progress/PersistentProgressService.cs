using Code.Runtime.Interactors;
using Code.Runtime.Repositories;

namespace Code.Runtime.Services.Progress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public IInteractorContainer InteractorContainer { get; set; } = new InteractorContainer();
    }
}