using System;
using Plugin.DocuFlow.Documentation;

namespace Code.Runtime.Repositories
{
    [Doc("The PlayerProgress class represents the progress data of the game. It contains various repositories for storing gameplay, settings, and purchases data.")]
    [Serializable]
    public class PlayerProgress
    {
        public GameplayData GameplayData = new GameplayData();
        public SettingsRepository SettingsRepository = new SettingsRepository();
        public PurchasesRepository PurchasesRepository = new PurchasesRepository();
    }

    [Serializable]
    public class GameplayData
    {
        public ScoreRepository ScoreRepository = new ScoreRepository();
        public ShapeRepository ShapeRepository = new ShapeRepository();
        public MoneyRepository MoneyRepository = new MoneyRepository();
        public GameplayShapesRepository GameplayShapesRepository = new GameplayShapesRepository();
        public RankingRepository RankingRepository = new RankingRepository();
    }
}