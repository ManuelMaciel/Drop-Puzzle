using Code.Runtime.Configs;
using Code.Runtime.Interactors;
using CodeBase.Services.StaticDataService;

namespace Code.Runtime.Repositories
{
    public class ProgressInitializer
    {
        public ProgressInitializer(IInteractorContainer interactorContainer, IStaticDataService staticDataService)
        {
            ScoreRepository scoreRepository = new ScoreRepository();
            ShapeRepository shapeRepository = new ShapeRepository();
            MoneyRepository moneyRepository = new MoneyRepository();

            interactorContainer.CreateInteractor<ShapeInteractor, ShapeRepository>(shapeRepository);
            interactorContainer.CreateInteractor<MoneyInteractor, MoneyRepository>(moneyRepository);
            interactorContainer.CreateInteractor<ScoreInteractor, ScoreRepository, ShapeScoreConfig>(scoreRepository,
                staticDataService.ShapeScoreConfig);
        }
    }
}