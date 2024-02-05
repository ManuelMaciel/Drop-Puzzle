using System.Collections.Generic;
using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using Code.Services.Progress;
using CodeBase.Services.StaticDataService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] private Transform purchasedBackgroundsContainer;
        [SerializeField] private PurchaseElement purchaseElementPrefab;

        private IStaticDataService _staticDataService;
        private IPersistentProgressService _persistentProgressService;

        private PurchasesInteractor _purchasesInteractor;

        private Dictionary<BackgroundType, PurchaseElement> _purchaseBackgroundElements =
            new Dictionary<BackgroundType, PurchaseElement>();

        [Inject]
        public void Construct(IStaticDataService staticDataService,
            IPersistentProgressService persistentProgressService)
        {
            _staticDataService = staticDataService;
            _persistentProgressService = persistentProgressService;
        }

        private void Start()
        {
            if (_staticDataService == null) return;

            _purchasesInteractor = _persistentProgressService.InteractorContainer.Get<PurchasesInteractor>();

            _purchasesInteractor.OnPurchasedBackground += UpdatePurchasedBackground;
            _purchasesInteractor.OnSelectedBackground += UpdateSelectedBackground;

            foreach (PurchasedBackground purchasedBackground in _staticDataService.PurchasedBackgroundsConfig
                .PurchasedBackgrounds)
            {
                PurchaseElement purchaseElement = Instantiate(purchaseElementPrefab, purchasedBackgroundsContainer);
                BackgroundType backgroundType = purchasedBackground.BackgroundType;

                purchaseElement.Initialize(
                    purchasedBackground.Background,
                    purchasedBackground.Price,
                    () => _purchasesInteractor.PurchaseBackground(backgroundType),
                    () => _purchasesInteractor.SelectBackground(backgroundType));
                
                _purchaseBackgroundElements.Add(backgroundType, purchaseElement);

                if (_purchasesInteractor.IsPurchasedBackground(backgroundType)) purchaseElement.Purchase();
                if (_purchasesInteractor.IsSelectedBackground(backgroundType)) purchaseElement.Select();
            }
        }

        private void OnDestroy()
        {
            _purchasesInteractor.OnPurchasedBackground -= UpdatePurchasedBackground;
            _purchasesInteractor.OnSelectedBackground -= UpdateSelectedBackground;
        }

        private void UpdateSelectedBackground(BackgroundType backgroundType)
        {
            foreach (var purchaseBackgroundsElement in _purchaseBackgroundElements.Values)
                purchaseBackgroundsElement.Unselect();

            _purchaseBackgroundElements[backgroundType].Select();
        }

        private void UpdatePurchasedBackground(BackgroundType backgroundType)
        {
            _purchaseBackgroundElements[backgroundType].Purchase();
        }
    }
}