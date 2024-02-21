using Code.Runtime.Repositories;
using Code.Runtime.Services.Progress;
using Code.Runtime.Services.WindowsService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button CloseButton;

        protected IInteractorContainer _interactorContainer => _persistentProgressService.InteractorContainer;
        protected IPersistentProgressService _persistentProgressService;
        protected IWindowService _windowService;

        [Inject]
        public void Construct(IPersistentProgressService persistentProgressService, IWindowService windowService)
        {
            _persistentProgressService = persistentProgressService;
            _windowService = windowService;
        }

        private void Awake() =>
            Initialize();

        protected virtual void OnEnable()
        {
            CloseButton.onClick.AddListener(_windowService.Close);
            
            SubscribeUpdates();
        }

        protected virtual void OnDisable()
        {
            CloseButton.onClick.RemoveListener(_windowService.Close);
            
            Cleanup();
        }

        protected abstract void Initialize();
        protected abstract void SubscribeUpdates();
        protected abstract void Cleanup();
    }
}