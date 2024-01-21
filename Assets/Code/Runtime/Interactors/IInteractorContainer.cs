using Code.Runtime.Interactors;

namespace Code.Runtime.Repositories
{
    public interface IInteractorContainer
    {
        T Get<T>() where T : class, IInteractor;
        void CreateInteractor<T, TRepository>(IRepository repository) where T : Interactor<TRepository>, new();
        void CreateInteractor<T, TRepository, TPayload>(IRepository repository, TPayload payload)
            where T : PayloadInteractor<TRepository, TPayload>, new();
    }
}