using System;

namespace Code.Runtime.Repositories
{
    public class ScoreInteractor : IInteractor
    {
        public event Action OnScoreIncreased; 
        
        private readonly ScoreRepository _scoreRepository;

        public ScoreInteractor(ScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        public int GetMaxScore() => _scoreRepository.MaxScore;

        public int GetCurrentScore() => _scoreRepository.Score;
        
        public void AddScore(int score)
        {
            if (score < 0) return;

            _scoreRepository.Score += score;

            if (_scoreRepository.Score > _scoreRepository.MaxScore)
                _scoreRepository.MaxScore = _scoreRepository.Score;
            
            OnScoreIncreased?.Invoke();
        }
    }
}