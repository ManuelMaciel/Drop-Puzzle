using System.Collections.Generic;

namespace Code.Services.SaveLoadService
{
    public interface ISaver<T> where T: class
    {
        IEnumerable<string> GetAll { get; }

        void Save(string key, T data);

        T Load(string key);
    }
}