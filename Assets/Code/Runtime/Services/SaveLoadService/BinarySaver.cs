using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Code.Runtime.Services.SaveLoadService
{
    public class BinarySaver<T> : ISaver<T> where T : class
    {
        private readonly BinaryFormatter _formatter;
    
        public BinarySaver()
        {
            Directory.CreateDirectory(SaveUtility.SaveDirectory);
            _formatter = new BinaryFormatter();
        }

        public IEnumerable<string> GetAll => Directory.GetFiles(SaveUtility.SaveDirectory);

        public void Save(string key, T data)
        {
            string savePath = GetPath(key);
        
            using (FileStream stream = File.Create(savePath))
            {
                _formatter.Serialize(stream, data);
            }
        }

        public T Load(string key)
        {
            string path = GetPath(key);

            if (File.Exists(path))
            {
                T returnObj;
                using (FileStream file = File.Open(path, FileMode.Open))
                {
                    object loadedData = _formatter.Deserialize(file);
                    returnObj = (T) loadedData;
                }

                return returnObj;
            }

            return null;
        }
    
        private string GetPath(string key)
        {
            return SaveUtility.SaveDirectory + key + ".dat";
        }
    }
}