using System;
using Code.Runtime.Configs;
using Code.Runtime.Repositories;
using Code.Runtime.Services.SaveLoadService;

namespace Code.Runtime.Interactors
{
    public class PurchasesInteractor : PayloadInteractor<PurchasesRepository, PurchasesInteractor.Payload>
    {
        public event Action<BackgroundType> OnPurchasedBackground;
        public event Action<BackgroundType> OnSelectedBackground;

        private PurchasedBackgroundsConfig _purchasedBackgroundsConfig;
        private MoneyInteractor _moneyInteractor;
        private ISaveLoadService _saveLoadService;

        public override void Initialize(Payload payload)
        {
            _purchasedBackgroundsConfig = payload.PurchasedBackgroundsConfig;
            _moneyInteractor = payload.MoneyInteractor;
            _saveLoadService = payload.SaveLoadService;
        }

        public void SelectBackground(BackgroundType backgroundType)
        {
            if (!IsPurchasedBackground(backgroundType)) return;

            _repository.SelectedBackground = backgroundType;

            OnSelectedBackground?.Invoke(backgroundType);
            
            _saveLoadService.SaveProgress();
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
            
            _saveLoadService.SaveProgress();
        }

        public struct Payload
        {
            public PurchasedBackgroundsConfig PurchasedBackgroundsConfig;
            public MoneyInteractor MoneyInteractor;
            public ISaveLoadService SaveLoadService;

            public Payload(PurchasedBackgroundsConfig purchasedBackgroundsConfig, MoneyInteractor moneyInteractor,
                ISaveLoadService saveLoadService)
            {
                PurchasedBackgroundsConfig = purchasedBackgroundsConfig;
                MoneyInteractor = moneyInteractor;
                SaveLoadService = saveLoadService;
            }
        }
    }
}