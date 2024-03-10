using UnityEditor;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    private const string BuildPathWebGL = "D:\\Unity\\Projects\\Builds\\WebGL";
    private const string BuildPathAndroid = "D:\\Unity\\Projects\\Builds\\Android";
    
    [MenuItem("Tools/Build WebGL")]
    public static void BuildWebGL()
    {
        string[] scenes = GetScenes();
        BuildPipeline.BuildPlayer(scenes, BuildPathWebGL, BuildTarget.WebGL, BuildOptions.None);
    }
    
    [MenuItem("Tools/Build Android")]
    public static void BuildAndroid()
    {
        string[] scenes = GetScenes();
        BuildPipeline.BuildPlayer(scenes, BuildPathAndroid, BuildTarget.Android, BuildOptions.None);
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