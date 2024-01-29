using Code.Runtime.UI.Windows;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _instantiator;

        private Transform _uiRoot;

        UIFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void CreateUIRoot()
        {
            _uiRoot = InstantiateWindow<Transform>(InfrastructureAssetPath.UIRootPath);
        }

        public WindowBase CreateTest()
        {
            TestWindow instantiateWindow =
                InstantiateWindow<TestWindow>(InfrastructureAssetPath.TestWindowPath, _uiRoot);

            return instantiateWindow;
        }

        private T InstantiateWindow<T>(string path, Transform parent = null) where T : Object
        {
            T prefab = Resources.Load<T>(path);
            return _instantiator.InstantiatePrefabForComponent<T>(prefab, parent);
        }
    }
}