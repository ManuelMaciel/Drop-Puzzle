using Code.Runtime.Repositories;
using Plugin.DocuFlow.Documentation;

namespace Code.Runtime.Interactors
{
    public interface IInteractor
    {
        void Construct(IRepository repository);
    }

    [Doc("Interactors are responsible for safe interacting with repositories")]
    public abstract class Interactor<TRepository> : IInteractor where TRepository : IRepository
    {
        protected TRepository _repository;
        
        public void Construct(IRepository repository)
        {
            _repository = (TRepository) repository;
        }
    }
    
    public abstract class PayloadInteractor<TRepository, TPayload> : IInteractor
    {
        protected TRepository _repository;
        
        public void Construct(IRepository repository)
        {
            _repository = (TRepository) repository;
        }

        public abstract void Initialize(TPayload payload);
    }
}