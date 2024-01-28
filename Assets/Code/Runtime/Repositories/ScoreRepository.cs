using System;

namespace Code.Runtime.Repositories
{
    [Serializable]
    public class ScoreRepository : IRepository
    {
        public int Score;
        public int MaxScore;
    }
}