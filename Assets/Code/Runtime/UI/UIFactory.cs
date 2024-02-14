﻿using System;
using System.Collections.Generic;
using Code.Runtime.UI.Windows;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Runtime.UI
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _instantiator;

        private Transform _uiRoot;

        private Dictionary<Type, string> windowsPath = new Dictionary<Type, string>()
        {
            [typeof(TestWindow)] = InfrastructureAssetPath.TestWindowPath,
            [typeof(RestartGameWindow)] = InfrastructureAssetPath.RestartGameWindowPath,
            [typeof(ShopWindow)] = InfrastructureAssetPath.ShopWindowPath,
        };

        UIFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void CreateWindowsRoot()
        {
            _uiRoot = Instantiate<Transform>(InfrastructureAssetPath.WindowsRootPath);
        }
        
        public T CreateWindow<T>() where T : WindowBase
        {
            T instantiateWindow =
                Instantiate<T>(windowsPath[typeof(T)], _uiRoot);

            return instantiateWindow;
        }

        private T Instantiate<T>(string path, Transform parent = null) where T : Object
        {
            T prefab = Resources.Load<T>(path);
            return _instantiator.InstantiatePrefabForComponent<T>(prefab, parent);
        }
    }
}