using UnityEngine;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public interface IGameObjectPool<T> : IObjectPool<T>
    {
        public T Get(Vector3 at);
    }
}