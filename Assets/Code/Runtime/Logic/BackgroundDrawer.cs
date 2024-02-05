using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Services.Progress;
using CodeBase.Services.StaticDataService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BackgroundDrawer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;

        private PurchasesInteractor _purchasesInteractor;

        [Inject]
        public void Construct(IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
        }

        private void Awake() =>
            _spriteRenderer = this.GetComponent<SpriteRenderer>();

        private void Start()
        {
            if (_progressService == null) return;

            _purchasesInteractor = _progressService.InteractorContainer.Get<PurchasesInteractor>();

            _purchasesInteractor.OnSelectedBackground += SelectedBackground;

            SelectedBackground(_purchasesInteractor.GetSelectedBackground());
        }

        private void OnDestroy()
        {
            if (_purchasesInteractor != null)
                _purchasesInteractor.OnSelectedBackground -= SelectedBackground;
        }

        private void SelectedBackground(BackgroundType backgroundType)
        {
            _spriteRenderer.sprite = _staticDataService
                .PurchasedBackgroundsConfig
                .GetByBackgroundType(backgroundType)
                .Background;
        }
    }
}