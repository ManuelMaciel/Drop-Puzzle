using System;
using Code.Runtime.Configs;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class PurchasesInteractor : PayloadInteractor<PurchasesRepository, PurchasesInteractor.Payload>
    {
        public event Action<BackgroundType> OnPurchasedBackground;
        public event Action<BackgroundType> OnSelectedBackground;

        private PurchasedBackgroundsConfig _purchasedBackgroundsConfig;
        private MoneyInteractor _moneyInteractor;

        public override void Initialize(Payload payload)
        {
            _purchasedBackgroundsConfig = payload.PurchasedBackgroundsConfig;
            _moneyInteractor = payload.MoneyInteractor;
        }

        public void SelectBackground(BackgroundType backgroundType)
        {
            if (!IsPurchasedBackground(backgroundType)) return;

            _repository.SelectedBackground = backgroundType;
            
            OnSelectedBackground?.Invoke(backgroundType);
        }

        public void PurchaseBackground(BackgroundType purchasedBackground)
        {
            int price = _purchasedBackgroundsConfig.GetByBackgroundType(purchasedBackground).Price;
            
            if (!_moneyInteractor.EnoughCoins(price)) return;

            _moneyInteractor.Spend(price);
            AddBackground(purchasedBackground);
        }

        public bool IsPurchasedBackground(BackgroundType purchasedBackground) =>
            _repository.PurchasedBackgrounds.Contains(purchasedBackground);

        public bool IsSelectedBackground(BackgroundType backgroundType) =>
            _repository.SelectedBackground == backgroundType;

        public BackgroundType GetSelectedBackground() => 
            _repository.SelectedBackground;

        private void AddBackground(BackgroundType purchasedBackground)
        {
            _repository.PurchasedBackgrounds.Add(purchasedBackground);

            OnPurchasedBackground?.Invoke(purchasedBackground);
        }

        public class Payload
        {
            public PurchasedBackgroundsConfig PurchasedBackgroundsConfig;
            public MoneyInteractor MoneyInteractor;

            public Payload(PurchasedBackgroundsConfig purchasedBackgroundsConfig, MoneyInteractor moneyInteractor)
            {
                PurchasedBackgroundsConfig = purchasedBackgroundsConfig;
                MoneyInteractor = moneyInteractor;
            }
        }
    }
}