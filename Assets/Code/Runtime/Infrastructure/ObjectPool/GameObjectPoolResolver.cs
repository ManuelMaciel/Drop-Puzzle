using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class GameObjectPoolResolver<T> : GameObjectPool<T> where T : Component
    {
        private readonly DiContainer _diContainer;

        public GameObjectPoolResolver(T @object, int preloadCount, IGameObjectsPoolContainer gameObjectsPoolContainer,
            DiContainer diContainer) :
            base(@object, preloadCount, gameObjectsPoolContainer)
        {
            _diContainer = diContainer;
        }

        protected override T PreloadAction()
        {
            GameObject instantiatePrefab = _diContainer.InstantiatePrefab(_poolObject);

            return instantiatePrefab.GetComponent<T>();
        }
    }
}