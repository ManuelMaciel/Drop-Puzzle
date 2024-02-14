using Code.Runtime.UI.Windows;

namespace Code.Runtime.UI
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        T CreateWindow<T>() where T : WindowBase;
    }
}