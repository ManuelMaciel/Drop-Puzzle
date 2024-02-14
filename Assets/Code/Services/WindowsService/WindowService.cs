using System.Collections.Generic;
using System.Linq;
using Code.Runtime.UI;
using Code.Runtime.UI.Windows;

namespace Code.Services.WindowsService
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;
        private WindowBase _currentWindow;
        private WindowType _currentWindowType;

        private List<WindowType> _previousPages = new();
        private Dictionary<WindowType, WindowBase> _createdWindows = new();

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            _uiFactory.CreateWindowsRoot();
        }

        public void Open(WindowType windowType, bool returnPage = false)
        {
            SetWindow(windowType, returnPage);

            if(TryShowCreatedWindow(windowType)) return;
            
            switch (windowType)
            {
                case WindowType.Unknown:
                    break;
                case WindowType.Test:
                    _currentWindow = _uiFactory.CreateWindow<TestWindow>();
                    break;
                case WindowType.RestartGame:
                    _currentWindow = _uiFactory.CreateWindow<RestartGameWindow>();
                    break;
                case WindowType.Shop:
                    _currentWindow = _uiFactory.CreateWindow<ShopWindow>();
                    break;
            }
            
            _createdWindows.Add(windowType, _currentWindow);
        }

        private bool TryShowCreatedWindow(WindowType windowType)
        {
            bool isCreatedWindow = (_createdWindows.TryGetValue(windowType, out WindowBase windowObject));

            if(isCreatedWindow)
            {
                windowObject.gameObject.SetActive(true);

                _currentWindow = windowObject;
            }

            return isCreatedWindow;
        }

        public void Close()
        {
            DestroyWindow();

            if (_previousPages.Count > 0)
            {
                WindowType windowType = _previousPages.Last();
                _previousPages.Remove(windowType);
                Open(windowType);
            }
        }

        private void DestroyWindow()
        {
            _currentWindow.gameObject.SetActive(false);

            _currentWindow = null;
        }

        private void SetWindow(WindowType windowType, bool returnPage)
        {
            if (_currentWindow != null)
            {
                if (returnPage) _previousPages.Add(_currentWindowType);

                DestroyWindow();
            }

            _currentWindowType = windowType;
        }
    }
}