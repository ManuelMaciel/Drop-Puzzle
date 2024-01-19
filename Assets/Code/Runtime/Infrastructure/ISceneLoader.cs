using System;

namespace Code.Runtime.Infrastructure
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
    }
}