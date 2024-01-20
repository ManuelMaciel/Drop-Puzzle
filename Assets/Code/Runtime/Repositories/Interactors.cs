using System;
using System.Collections.Generic;

namespace Code.Runtime.Repositories
{
    public class Interactors
    {
        private Dictionary<Type, IInteractor> _interactors = new Dictionary<Type, IInteractor>();

        public T Get<T>() where T : class, IInteractor =>
            _interactors[typeof(T)] as T;

        public void Register<T>(T interactor) where T : IInteractor =>
            _interactors.Add(typeof(T), interactor);
    }
}