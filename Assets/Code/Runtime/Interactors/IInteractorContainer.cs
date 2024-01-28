using Code.Runtime.Interactors;

namespace Code.Runtime.Repositories
{
    public interface IInteractorContainer
    {
        T Get<T>() where T : class, IInteractor;
        void CreateInteractor<T, TRepository>(TRepository repository) where T : Interactor<TRepository>, new();
        void CreateInteractor<T, TRepository, TPayload>(TRepository repository, TPayload payload)
            where T : PayloadInteractor<TRepository, TPayload>, new();

        bool TryGet<T>(out IInteractor interactor) where T : class, IInteractor;
    }
}