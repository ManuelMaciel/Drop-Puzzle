using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public interface IInteractor
    {
        void Construct(IRepository repository);
    }

    public abstract class Interactor<TRepository> : IInteractor
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