using System;

namespace Code.Runtime.Repositories
{
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
    }
}