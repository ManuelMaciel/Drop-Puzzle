using System;
using System.Collections.Generic;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class InteractorContainer : IInteractorContainer
    {
        private Dictionary<Type, IInteractor>
            _interactors = new Dictionary<Type, IInteractor>();

        public T Get<T>() where T : class, IInteractor =>
            _interactors[typeof(T)] as T;

        public bool TryGet<T>(out IInteractor interactor) where T : class, IInteractor =>
            _interactors.TryGetValue(typeof(T), out interactor);

        public void CreateInteractor<T, TRepository>(TRepository repository) where T : Interactor<TRepository>, new()
        {
            T interactor = new T();

            ConstructInteractor<T>(interactor, (IRepository)repository);
        }

        public void CreateInteractor<T, TRepository, TPayload>(TRepository repository, TPayload payload)
            where T : PayloadInteractor<TRepository, TPayload>, new()
        {
            T interactor = new T();

            ConstructInteractor<T>(interactor, (IRepository)repository);

            interactor.Initialize(payload);
        }

        private void ConstructInteractor<T>(IInteractor interactor, IRepository repository)
        {
            interactor.Construct(repository);
            _interactors.Add(typeof(T), interactor);
        }
    }
}