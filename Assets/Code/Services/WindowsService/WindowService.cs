using System.Collections.Generic;
using System.Linq;
using Code.Runtime.UI;
using Code.Runtime.UI.Windows;
using UnityEngine;

namespace Code.Services.WindowsService
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;
        private WindowBase _currentWindow;
        private WindowType _currentWindowType;

        private List<WindowType> _previousPages = new List<WindowType>();

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            _uiFactory.CreateUIRoot();
        }

        public void Open(WindowType windowType, bool returnPage = false)
        {
            SetWindow(windowType, returnPage);

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
            Object.Destroy(_currentWindow.gameObject);

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