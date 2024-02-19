using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class ComponentPoolResolver<T> : ComponentPool<T> where T : Component
    {
        private readonly DiContainer _diContainer;

        public ComponentPoolResolver(T @object, int preloadCount, IGameObjectsPoolContainer gameObjectsPoolContainer,
            DiContainer diContainer) :
            base(@object, preloadCount, gameObjectsPoolContainer)
        {
            _diContainer = diContainer;
        }

        protected override T PreloadAction()
        {
            GameObject instantiatePrefab = _diContainer.InstantiatePrefab(_poolObject);
            T component = instantiatePrefab.GetComponent<T>();
            
            ReturnToPoolContainer(component);

            return component;
        }
    }
}