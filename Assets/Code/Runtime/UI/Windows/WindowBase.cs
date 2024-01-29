using Code.Runtime.Repositories;
using Code.Services.Progress;
using Code.Services.WindowsService;
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

        private IWindowService _windowService;

        [Inject]
        public void Construct(IPersistentProgressService persistentProgressService, IWindowService windowService)
        {
            _persistentProgressService = persistentProgressService;
            _windowService = windowService;
        }
        
        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        protected virtual void OnAwake()
        {
            CloseButton.onClick.AddListener(_windowService.Close);
        }

        protected abstract void Initialize();
        protected abstract void SubscribeUpdates();
        protected abstract void Cleanup();
    }
}