using Code.Runtime.Services.SaveLoadService;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    public class Tools 
    {
        [MenuItem("Tools/ClearSaves")]
        public static void ClearSaves()
        {
            SaveUtility.DeleteAllSaves();
        }
    }
}