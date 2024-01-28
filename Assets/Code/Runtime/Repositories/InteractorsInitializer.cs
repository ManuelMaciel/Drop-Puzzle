using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using CodeBase.Services.StaticDataService;

namespace Code.Runtime.Repositories
{
    public static class InteractorsInitializer
    {
        public static void Initialize(PlayerProgress playerProgress, IInteractorContainer interactorContainer,
            IStaticDataService staticDataService)
        {
            interactorContainer.CreateInteractor<ShapeInteractor, ShapeRepository>(playerProgress.ShapeRepository);
            interactorContainer.CreateInteractor<MoneyInteractor, MoneyRepository>(playerProgress.MoneyRepository);
            interactorContainer.CreateInteractor<ScoreInteractor, ScoreRepository, ShapeScoreConfig>(playerProgress.ScoreRepository,
                staticDataService.ShapeScoreConfig);
        }
    }
}