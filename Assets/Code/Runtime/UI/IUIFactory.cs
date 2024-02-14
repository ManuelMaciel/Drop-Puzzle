using Code.Runtime.UI.Windows;

namespace Code.Runtime.UI
{
    public interface IUIFactory
    {
        void CreateWindowsRoot();
        T CreateWindow<T>() where T : WindowBase;
    }
}