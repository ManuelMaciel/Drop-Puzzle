using System;
using UnityEngine.SceneManagement;

namespace Code.Runtime.Infrastructure
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
        bool IsNameLoadedScene(string sceneName);
    }
}