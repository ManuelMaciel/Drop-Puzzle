using Code.Runtime.Repositories;

namespace Code.Services.Progress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public Interactors Interactors { get; set; } = new Interactors();
    }
}