using Code.Runtime.UI.Windows;
using UnityEngine;

namespace Code.Runtime.UI
{
    public interface IUIFactory
    {
        void Initialize();
        Transform CreateWindowsRoot();
        T CreateWindow<T>(WindowType windowType) where T : WindowBase;
    }
}