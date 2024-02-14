using System.Collections.Generic;
using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using CodeBase.Services.StaticDataService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.UI.Windows
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private Transform purchasedBackgroundsContainer;
        [SerializeField] private PurchaseElement purchaseElementPrefab;

        private IStaticDataService _staticDataService;
        private PurchasesInteractor _purchasesInteractor;

        private Dictionary<BackgroundType, PurchaseElement> _purchaseBackgroundElements =
            new Dictionary<BackgroundType, PurchaseElement>();

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        private void OnDestroy()
        {
            _purchasesInteractor.OnPurchasedBackground -= UpdatePurchasedBackground;
            _purchasesInteractor.OnSelectedBackground -= UpdateSelectedBackground;
        }

        protected override void Initialize()
        {
            _purchasesInteractor = _persistentProgressService.InteractorContainer.Get<PurchasesInteractor>();

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

        protected override void SubscribeUpdates()
        {
            _purchasesInteractor.OnPurchasedBackground += UpdatePurchasedBackground;
            _purchasesInteractor.OnSelectedBackground += UpdateSelectedBackground;
        }

        protected override void Cleanup()
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