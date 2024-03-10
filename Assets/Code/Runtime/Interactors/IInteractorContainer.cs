using Code.Runtime.Interactors;

namespace Code.Runtime.Repositories
{
    public interface IInteractorContainer
    {
        bool TryGet<T>(out T value) where T : class, IInteractor;
        T Get<T>() where T : class, IInteractor;
        void CreateInteractor<T, TRepository>(TRepository repository) where T : Interactor<TRepository>, new() where TRepository : IRepository;
        void CreateInteractor<T, TRepository, TPayload>(TRepository repository, TPayload payload)
            where T : PayloadInteractor<TRepository, TPayload>, new();
    }
}