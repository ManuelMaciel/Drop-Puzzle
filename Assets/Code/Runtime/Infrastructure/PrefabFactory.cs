using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Runtime.Infrastructure
{
    public class PrefabFactory<TComponent> : IFactory<string, TComponent>
    {
        public TComponent InstantiatedPrefab { get; private set; }
        
        private readonly DiContainer _container;

        public PrefabFactory(DiContainer container)
        {
            _container = container;
        }

        public TComponent Create(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            
            if (prefab == null)
                throw new Exception($"Prefab not found at path: {path}");
            
            GameObject newObject = _container.InstantiatePrefab(prefab);
            SceneManager.MoveGameObjectToScene(newObject, SceneManager.GetActiveScene());
            InstantiatedPrefab = newObject.GetComponent<TComponent>();

            return InstantiatedPrefab;
        }
    }
}