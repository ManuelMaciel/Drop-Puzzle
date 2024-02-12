namespace Code.Runtime.Infrastructure.ObjectPool
{
    public interface IObjectPool<T>
    {
        T Get();
        void Return(T item);
    }
}