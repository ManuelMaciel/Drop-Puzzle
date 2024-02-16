using UnityEngine;

namespace Code.Runtime.Infrastructure.ObjectPool
{
    public interface IGameObjectsPoolContainer
    {
        Transform CreatePoolContainer<T>(T @object) where T : Component;
        void AddInPoolContainer<T>(T @object, Transform poolContainer) where T : Component;
    }
}