using UnityEngine;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public interface IGlobalGameObjectPool
    {
        void AddPoolContainer(Transform poolContainer);
        T CreateObject<T>(T @object, Transform poolContainer) where T : Object;
    }
}