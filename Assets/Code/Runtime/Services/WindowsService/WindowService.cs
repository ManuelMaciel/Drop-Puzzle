using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Configs;
using Code.Runtime.Services.AudioService;
using Code.Runtime.Services.LogService;
using Code.Runtime.UI;
using Code.Runtime.UI.Windows;
using Plugin.DocuFlow.Documentation;

namespace Code.Runtime.Services.WindowsService
{
    [Doc("The WindowService class provides functionality for managing UI windows within the game. It handles the creation, opening, and closing of different types of windows, including animations for transitions between them.")]
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private readonly ILogService _logService;
        private readonly IAudioService _audioService;

        private WindowBase _currentWindow;
        private WindowType _currentWindowType;
        private IWindowsAnimation _windowsAnimation;

        private List<WindowType> _previousPages = new();
        private Dictionary<WindowType, WindowBase> _createdWindows = new();

        public WindowService(IUIFactory uiFactory, ILogService logService, IAudioService audioService)
        {
            _uiFactory = uiFactory;
            _logService = logService;
            _audioService = audioService;
        }
        
        public void Initialize()
        {
            var windowsRoot = _uiFactory.CreateWindowsRoot();

            _windowsAnimation = new WindowsAnimation(windowsRoot);
        }

        public void Open(WindowType windowType, bool returnPage = false)
        {
            _audioService.PlaySfx(SfxType.PopUp);

            SetWindow(windowType, returnPage);

            if (!TryShowCreatedWindow(windowType))
            {
                switch (windowType)
                {
                    case WindowType.RestartGame:
                        _currentWindow = _uiFactory.CreateWindow<RestartGameWindow>(windowType);
                        break;
                    case WindowType.Shop:
                        _currentWindow = _uiFactory.CreateWindow<ShopWindow>(windowType);
                        break;
                    case WindowType.Settings:
                        _currentWindow = _uiFactory.CreateWindow<SettingsWindow>(windowType);
                        break;
                    case WindowType.Rankings:
                        _currentWindow = _uiFactory.CreateWindow<RankingWindow>(windowType);
                        break;
                }

                _createdWindows.Add(windowType, _currentWindow);
            }

            _windowsAnimation.OpenAnimation(_currentWindow.transform);
        }

        private bool TryShowCreatedWindow(WindowType windowType)
        {
            bool isCreatedWindow = (_createdWindows.TryGetValue(windowType, out WindowBase windowObject));

            if (isCreatedWindow)
            {
                windowObject.gameObject.SetActive(true);
                
                _currentWindow = windowObject;
            }

            return isCreatedWindow;
        }

        public void Close()
        {
            if (_currentWindow == null)
            {
                _logService.LogError("Window is closed");

                return;
            }

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
            _windowsAnimation.CloseAnimation(_currentWindow.transform, () =>
            {
                _currentWindow.gameObject.SetActive(false);
                _currentWindow = null;
            });
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