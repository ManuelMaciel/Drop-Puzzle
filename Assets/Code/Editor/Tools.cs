using Code.Runtime.Services.SaveLoadService;
using UnityEditor;

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