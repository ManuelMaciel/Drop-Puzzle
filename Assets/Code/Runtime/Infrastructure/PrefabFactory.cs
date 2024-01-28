using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Runtime.Infrastructure
{
    public class PrefabFactory<TComponent> : IFactory<string, TComponent>
    {
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
            
            GameObject newObject = Object.Instantiate(prefab);
            TComponent component = newObject.GetComponent<TComponent>();
            
            _container.Inject(component);

            return component;
        }
    }
}