using Code.Runtime.Configs;
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

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        private void Start()
        {
            if(_staticDataService == null) return;

            Debug.Log(_staticDataService.PurchasedBackgroundsConfig);
            
            foreach (PurchasedBackground purchasedBackground in _staticDataService.PurchasedBackgroundsConfig
                .PurchasedBackgrounds)
            {
                PurchaseElement purchaseElement = Instantiate(purchaseElementPrefab, purchasedBackgroundsContainer);
                
                purchaseElement.Initialize(purchasedBackground.Background, purchasedBackground.Price);
            }
        }
    }
}