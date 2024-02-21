using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.UI;
using UnityEngine;

namespace Code.Runtime.Configs
{
    [CreateAssetMenu(fileName = "WindowAssetsConfig", menuName = "Configs/WindowAssetsConfig")]
    public class WindowAssetsConfig : ScriptableObject
    {
        public GameObject WindowRootPrefab;
        public List<WindowAsset> WindowAssets;
        
        private void OnValidate()
        {
            Array arrayTypes = Enum.GetValues(typeof(WindowType));

            if (WindowAssets == null || WindowAssets.Count < arrayTypes.Length)
            {
                foreach (WindowType windowType in arrayTypes)
                {
                    if (WindowAssets.Any(sd => sd.WindowType == windowType)) continue;

                    WindowAssets.Add(new WindowAsset()
                    {
                        WindowType = windowType,
                    });
                }
            }
        }
    }

    [Serializable]
    public struct WindowAsset
    {
        public WindowType WindowType;
        public GameObject WindowPrefab;
    }
}