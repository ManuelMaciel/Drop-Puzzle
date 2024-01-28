using System;

namespace Code.Runtime.Repositories
{
    [Serializable]
    public class PlayerProgress
    {
        public ScoreRepository ScoreRepository = new ScoreRepository();
        public ShapeRepository ShapeRepository = new ShapeRepository();
        public MoneyRepository MoneyRepository = new MoneyRepository();
    }
}