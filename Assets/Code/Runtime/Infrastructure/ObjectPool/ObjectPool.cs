using System;
using System.Collections.Generic;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public class ObjectPool<T> : IObjectPool<T>
    {
        private readonly Action<T> _returnAction;
        private readonly Action<T> _getAction;
        private readonly int _preloadCount;
        
        private Func<T> _preloadFunc;

        private Queue<T> _pool = new();

        public ObjectPool(Action<T> getAction, Action<T> returnAction, int preloadCount)
        {
            _getAction = getAction;
            _returnAction = returnAction;
            _preloadCount = preloadCount;
        }

        public void Initialize(Func<T> preloadFunc)
        {
            if (preloadFunc == null)
            {
                throw new NullReferenceException("Preload function is null");
            }

            _preloadFunc = preloadFunc;   
        }

        public T Get()
        {
            T item = _pool.Count > 0 ? _pool.Dequeue() : _preloadFunc();
            _getAction(item);

            return item;
        }

        public void Return(T item)
        {
            _returnAction(item);
            _pool.Enqueue(item);
        }

        protected void SpawnObjects()
        {
            for (int i = 0; i < _preloadCount; i++) Return(_preloadFunc());
        }
    }
}