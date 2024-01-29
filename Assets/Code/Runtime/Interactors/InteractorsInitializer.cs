using Code.Runtime.Configs;
using Code.Runtime.Repositories;
using CodeBase.Services.StaticDataService;

namespace Code.Runtime.Interactors
{
    public static class InteractorsInitializer
    {
        public static void Initialize(PlayerProgress playerProgress, IInteractorContainer interactorContainer,
            IStaticDataService staticDataService)
        {
            interactorContainer.CreateInteractor<ShapeInteractor, ShapeRepository>(playerProgress.ShapeRepository);
            interactorContainer.CreateInteractor<MoneyInteractor, MoneyRepository>(playerProgress.MoneyRepository);
            interactorContainer.CreateInteractor<GameplayShapesInteractor, GameplayShapesRepository>(playerProgress.GameplayShapesRepository);
            interactorContainer.CreateInteractor<ScoreInteractor, ScoreRepository, ShapeScoreConfig>(playerProgress.ScoreRepository,
                staticDataService.ShapeScoreConfig);
        }
    }
}