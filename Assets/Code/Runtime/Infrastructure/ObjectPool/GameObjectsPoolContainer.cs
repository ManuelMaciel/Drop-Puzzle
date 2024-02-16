using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class GameObjectsPoolContainer : IGameObjectsPoolContainer
    {
        private const string ObjectPoolsName = "ObjectPools";

        private readonly DiContainer _diContainer;
        
        private Transform _objectPools;

        public GameObjectsPoolContainer(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _objectPools = new GameObject(ObjectPoolsName).transform;
        }

        public Transform CreatePoolContainer<T>(T @object) where T : Component
        {
            Transform poolContainer = new GameObject($"{@object.name} ({@object.GetType()})").transform;

            poolContainer.SetParent(_objectPools);

            return poolContainer;
        }
        
        public void AddInPoolContainer<T>(T @object, Transform poolContainer) where T : Component =>
            @object.transform.SetParent(poolContainer);
    }
}