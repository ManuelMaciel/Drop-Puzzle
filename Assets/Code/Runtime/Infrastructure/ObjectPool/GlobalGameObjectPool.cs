using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class GlobalGameObjectPool : IGlobalGameObjectPool
    {
        private readonly DiContainer _diContainer;
        private const string ObjectPoolsName = "ObjectPools";

        private Transform _objectPools;

        public GlobalGameObjectPool(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _objectPools = new GameObject(ObjectPoolsName).transform;
        }

        public void AddPoolContainer(Transform poolContainer)
        {
            poolContainer.SetParent(_objectPools);
        }
        
        public T CreateObject<T>(T @object, Transform poolContainer) where T : Component
        {
            var instantiate = Object.Instantiate(@object, poolContainer);

            _diContainer.InjectGameObject(instantiate.gameObject);
            
            return instantiate;
        }
    }
}