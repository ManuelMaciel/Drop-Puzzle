namespace Code.Runtime.Infrastructure.ObjectPool
{
    public interface IObjectPool<T>
    {
        void Initialize();
        T Get();
        void Return(T item);
    }
}