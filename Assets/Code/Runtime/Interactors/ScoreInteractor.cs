using System;
using Code.Runtime.Configs;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class ScoreInteractor : PayloadInteractor<ScoreRepository, ShapeScoreConfig>
    {
        public event Action<int> OnScoreIncreased;
        
        private ShapeScoreConfig _shapeScoreConfig;

        public override void Initialize(ShapeScoreConfig shapeScoreConfig)
        {
            _shapeScoreConfig = shapeScoreConfig;
        }

        public int GetMaxScore() => _repository.MaxScore;

        public int GetCurrentScore() => _repository.Score;

        public void AddScore(int score)
        {
            if (score < 0) return;

            _repository.Score += score;

            if (_repository.Score > _repository.MaxScore)
                _repository.MaxScore = _repository.Score;
            
            OnScoreIncreased?.Invoke(_repository.Score);
        }

        public void AddScoreByShapeSize(ShapeSize shapeSize)
        {
            AddScore(_shapeScoreConfig.Scores[(int)shapeSize]);
        }
    }
}