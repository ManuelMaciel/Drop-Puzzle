using Code.Runtime.Configs;
using Code.Runtime.Interactors;

namespace Code.Runtime.Repositories
{
    public class ProgressInitializer
    {
        public ProgressInitializer(IInteractorContainer interactorContainer, ShapeScoreConfig shapeScoreConfig)
        {
            ScoreRepository scoreRepository = new ScoreRepository();
            
            interactorContainer.CreateInteractor<ScoreInteractor, ScoreRepository, ShapeScoreConfig>(scoreRepository, shapeScoreConfig);
        } 
    }
}