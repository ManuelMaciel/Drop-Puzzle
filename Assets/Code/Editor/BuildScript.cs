using UnityEditor;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    private const string BuildPath = "D:\\Unity\\Projects\\Builds";
    
    [MenuItem("Tools/Build WebGL")]
    public static void BuildWebGL()
    {
        string[] scenes = GetScenes();
        BuildPipeline.BuildPlayer(scenes, BuildPath, BuildTarget.WebGL, BuildOptions.None);
    }
    
    [MenuItem("Tools/Build Android")]
    public static void BuildAndroid()
    {
        string[] scenes = GetScenes();
        BuildPipeline.BuildPlayer(scenes, BuildPath, BuildTarget.Android, BuildOptions.None);
    }

    private static string[] GetScenes()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }
}