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

        public void CreateInteractor<T, TRepository>(IRepository repository) where T : Interactor<TRepository>, new()
        {
            T interactor = new T();

            ConstructInteractor<T>(interactor, repository);
        }

        public void CreateInteractor<T, TRepository, TPayload>(IRepository repository, TPayload payload)
            where T : PayloadInteractor<TRepository, TPayload>, new()
        {
            T interactor = new T();

            ConstructInteractor<T>(interactor, repository);

            interactor.Initialize(payload);
        }

        private void ConstructInteractor<T>(IInteractor interactor, IRepository repository)
        {
            interactor.Construct(repository);
            _interactors.Add(typeof(T), interactor);
        }
    }
}