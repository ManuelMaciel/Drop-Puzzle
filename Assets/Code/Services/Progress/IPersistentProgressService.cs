using Code.Runtime.Repositories;

namespace Code.Services.Progress
{
    public interface IPersistentProgressService
    {
        Interactors Interactors { get; set; }
    }
}