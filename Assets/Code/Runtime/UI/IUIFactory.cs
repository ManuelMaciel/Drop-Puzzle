using Code.Runtime.UI.Windows;
using UnityEngine;

namespace Code.Runtime.UI
{
    public interface IUIFactory
    {
        Transform CreateWindowsRoot();
        T CreateWindow<T>() where T : WindowBase;
    }
}