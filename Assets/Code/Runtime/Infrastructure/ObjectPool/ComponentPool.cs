using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class ComponentPool<T> : IGameObjectPool<T> where T : Component
    {
        protected readonly T _poolObject;
        private readonly int _preloadCount;
        private readonly IGameObjectsPoolContainer _gameObjectsPoolContainer;

        private Queue<T> _pool = new();
        private Transform _poolContainer;

        public ComponentPool(T @object, int preloadCount, IGameObjectsPoolContainer gameObjectsPoolContainer)
        {
            _poolObject = @object;
            _preloadCount = preloadCount;
            _gameObjectsPoolContainer = gameObjectsPoolContainer;

            _poolContainer = gameObjectsPoolContainer.CreatePoolContainer(@object);
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
            ReturnToPoolContainer(item);
            _pool.Enqueue(item);
        }

        protected virtual T PreloadAction()
        {
            T createdObject = Object.Instantiate(_poolObject);

            ReturnToPoolContainer(createdObject);

            return createdObject;
        }

        protected void ReturnToPoolContainer(T createdObject) =>
            _gameObjectsPoolContainer.AddInPoolContainer(createdObject.transform, _poolContainer);

        private void ReturnAction(T @object)
            => @object.gameObject.SetActive(false);

        private void GetAction(T @object)
            => @object.gameObject.SetActive(true);

        private void SpawnObjects()
        {
            for (int i = 0; i < _preloadCount; i++)
                Return(PreloadAction());
        }
    }
}