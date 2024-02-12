using UnityEngine;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class GlobalGameObjectPool : IGlobalGameObjectPool
    {
        private const string ObjectPoolsName = "ObjectPools";

        private Transform _objectPools;

        public GlobalGameObjectPool()
        {
            _objectPools = new GameObject(ObjectPoolsName).transform;
        }

        public void AddPoolContainer(Transform poolContainer)
        {
            poolContainer.SetParent(_objectPools);
        }
        
        public T CreateObject<T>(T @object, Transform poolContainer) where T : Object
        {
            return Object.Instantiate(@object, poolContainer);
        }
    }
}