using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Configs;
using Code.Runtime.Services.StaticDataService;
using Code.Runtime.UI.Windows;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Runtime.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticDataService;

        private Transform _uiRoot;
        private WindowAssetsConfig _windowAssetsConfig;
        private Dictionary<WindowType, GameObject> _windowAssets = new ();

        UIFactory(IInstantiator instantiator, IStaticDataService staticDataService)
        {
            _instantiator = instantiator;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            _windowAssetsConfig = _staticDataService.WindowAssetsConfig;
            _windowAssets = _windowAssetsConfig.WindowAssets
                .ToDictionary(k => k.WindowType, v => v.WindowPrefab);
        }

        public Transform CreateWindowsRoot()
        {
            _uiRoot = Instantiate<Transform>(_windowAssetsConfig.WindowRootPrefab);

            return _uiRoot;
        }

        public T CreateWindow<T>(WindowType windowType) where T : WindowBase
        {
            T instantiateWindow =
                Instantiate<T>(_windowAssets[windowType], _uiRoot);

            return instantiateWindow;
        }

        private T Instantiate<T>(GameObject prefab, Transform parent = null) where T : Object
        {
            GameObject instantiatePrefab = _instantiator.InstantiatePrefab(prefab, parent);

            return instantiatePrefab.GetComponent<T>();
        }
    }
}