using System;
using System.Collections.Generic;
using Code.Runtime.Repositories;
using Plugin.DocuFlow.Documentation;

namespace Code.Runtime.Interactors
{
    [Doc("The InteractorContainer class provides functionality for managing interactors. It allows for creating and retrieving interactors by their types. Interactors are stored in a dictionary, and methods are provided for creating both basic and payload interactors.")]
    public class InteractorContainer : IInteractorContainer
    {
        private Dictionary<Type, IInteractor>
            _interactors = new Dictionary<Type, IInteractor>();

        public bool TryGet<T>(out T interactor) where T : class, IInteractor {
            if (_interactors.TryGetValue(typeof(T), out IInteractor value)) {
                interactor = value as T;
                return true;
            }
            interactor = null;
            return false;
        }

        public T Get<T>() where T : class, IInteractor =>
            _interactors[typeof(T)] as T;

        public void CreateInteractor<T, TRepository>(TRepository repository) where T : Interactor<TRepository>, new() where TRepository : IRepository
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