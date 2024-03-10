using Code.Runtime.Configs;
using Code.Runtime.Repositories;
using Code.Runtime.Services.SaveLoadService;
using Code.Runtime.Services.StaticDataService;
using Plugin.DocuFlow.Documentation;

namespace Code.Runtime.Interactors
{
    [Doc("The InteractorsInitializer class provides a static method for initializing interactors based on player progress, static data, and services. It encapsulates the logic for creating and registering interactors with the provided interactor container.")]
    public static class InteractorsInitializer
    {
        public static void Initialize(PlayerProgress playerProgress, IInteractorContainer interactorContainer,
            IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            interactorContainer.CreateInteractor<ShapeInteractor, ShapeRepository>(playerProgress.GameplayData
                .ShapeRepository);
            interactorContainer.CreateInteractor<MoneyInteractor, MoneyRepository, int>(playerProgress.GameplayData
                .MoneyRepository, staticDataService.AdConfig.Reward);
            interactorContainer.CreateInteractor<GameplayShapesInteractor, GameplayShapesRepository>(playerProgress
                .GameplayData.GameplayShapesRepository);
            interactorContainer.CreateInteractor<RankingInteractor, RankingRepository>(playerProgress.GameplayData
                .RankingRepository);
            interactorContainer.CreateInteractor<SettingsInteractor, SettingsRepository>(playerProgress
                .SettingsRepository);

            RegisterPurchasesInteractor(playerProgress, interactorContainer, staticDataService, saveLoadService);
            RegisterScoreInteractor(playerProgress, interactorContainer, staticDataService);
        }

        private static void RegisterScoreInteractor(PlayerProgress playerProgress,
            IInteractorContainer interactorContainer,
            IStaticDataService staticDataService)
        {
            interactorContainer.CreateInteractor<ScoreInteractor, ScoreRepository, ShapeScoreConfig>(
                playerProgress.GameplayData.ScoreRepository,
                staticDataService.ShapeScoreConfig);
        }

        private static void RegisterPurchasesInteractor(PlayerProgress playerProgress,
            IInteractorContainer interactorContainer,
            IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            interactorContainer.CreateInteractor<PurchasesInteractor, PurchasesRepository, PurchasesInteractor.Payload>(
                playerProgress.PurchasesRepository,
                new PurchasesInteractor.Payload(
                    staticDataService.PurchasedBackgroundsConfig,
                    interactorContainer.Get<MoneyInteractor>(), saveLoadService));
        }
    }
}