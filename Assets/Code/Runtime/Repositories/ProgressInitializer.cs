using Code.Runtime.Configs;
using Code.Runtime.Interactors;

namespace Code.Runtime.Repositories
{
    public class ProgressInitializer
    {
        public ProgressInitializer(IInteractorContainer interactorContainer, ShapeScoreConfig shapeScoreConfig)
        {
            ScoreRepository scoreRepository = new ScoreRepository();
            ShapeRepository shapeRepository = new ShapeRepository();
            MoneyRepository moneyRepository = new MoneyRepository();

            interactorContainer.CreateInteractor<ScoreInteractor, ScoreRepository, ShapeScoreConfig>(scoreRepository, shapeScoreConfig);
            interactorContainer.CreateInteractor<ShapeInteractor, ShapeRepository>(shapeRepository);
            interactorContainer.CreateInteractor<MoneyInteractor, MoneyRepository>(moneyRepository);
        } 
    }
}