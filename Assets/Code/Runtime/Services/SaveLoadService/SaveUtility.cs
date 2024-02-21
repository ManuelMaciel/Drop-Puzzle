using System.IO;
using UnityEngine;

namespace Code.Runtime.Services.SaveLoadService
{
    public static class SaveUtility
    {
        public static readonly string SaveDirectory = Application.persistentDataPath + "/Saves/";

        public static void DeleteAllSaves()
        {
            var filesPath = Directory.GetFiles(SaveDirectory);
            var directoriesPath = Directory.GetDirectories(SaveDirectory);

            foreach (string filePath in filesPath) 
                File.Delete(filePath);
        
            foreach (string directoryPath in directoriesPath)
                Directory.Delete(directoryPath);
        }
    }
}