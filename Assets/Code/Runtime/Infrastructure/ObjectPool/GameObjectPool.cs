using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class GameObjectPool<T> : IGameObjectPool<T> where T : Component
    {
        private readonly T _poolObject;
        private readonly int _preloadCount;
        private readonly IGlobalGameObjectPool _globalGameObjectPool;

        private Queue<T> _pool = new();
        private Transform _poolContainer;

        public GameObjectPool(T @object, int preloadCount, IGlobalGameObjectPool globalGameObjectPool)
        {
            _poolObject = @object;
            _preloadCount = preloadCount;
            _globalGameObjectPool = globalGameObjectPool;

            CreatePoolContainer(@object, globalGameObjectPool);
        }

        public void Initialize() => SpawnObjects();

        public T Get()
        {
            T item = _pool.Count > 0 ? _pool.Dequeue() : PreloadAction();
            GetAction(item);

            return item;
        }

        public T Get(Vector3 at)
        {
            T @object = Get();
            @object.transform.position = at;

            return @object;
        }

        public void Return(T item)
        {
            ReturnAction(item);
            _pool.Enqueue(item);
        }

        protected virtual T PreloadAction() =>
            _globalGameObjectPool.CreateObject(_poolObject, _poolContainer);

        private void ReturnAction(T @object)
            => @object.gameObject.SetActive(false);

        private void GetAction(T @object)
            => @object.gameObject.SetActive(true);

        private void CreatePoolContainer(T @object, IGlobalGameObjectPool globalGameObjectPool)
        {
            _poolContainer = new GameObject($"{@object.name} ({@object.GetType()})").transform;

            globalGameObjectPool.AddPoolContainer(_poolContainer);
        }

        private void SpawnObjects()
        {
            for (int i = 0; i < _preloadCount; i++)
                Return(PreloadAction());
        }
    }
}