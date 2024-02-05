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
        private Dictionary<BackgroundType, PurchaseElement> _purchaseBackgroundsElement =
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

            foreach (PurchasedBackground purchasedBackground in _staticDataService.PurchasedBackgroundsConfig
                .PurchasedBackgrounds)
            {
                PurchaseElement purchaseElement = Instantiate(purchaseElementPrefab, purchasedBackgroundsContainer);
                BackgroundType backgroundType = purchasedBackground.BackgroundType;

                purchaseElement.Initialize(purchasedBackground.Background, purchasedBackground.Price);
                _purchaseBackgroundsElement.Add(backgroundType, purchaseElement);

                purchaseElement.OnPurchased += () => { _purchasesInteractor.PurchaseBackground(backgroundType); };

                if (_purchasesInteractor.IsPurchasedBackground(backgroundType)) purchaseElement.Purchased();
            }
        }

        private void UpdatePurchasedBackground()
        {
            foreach (KeyValuePair<BackgroundType, PurchaseElement> purchaseElement in _purchaseBackgroundsElement)
                if (_purchasesInteractor.IsPurchasedBackground(purchaseElement.Key))
                    purchaseElement.Value.Purchased();
        }
    }
}