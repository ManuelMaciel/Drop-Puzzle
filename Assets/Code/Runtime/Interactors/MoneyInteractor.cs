using System;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class MoneyInteractor : PayloadInteractor<MoneyRepository, int>
    {
        public event Action<int> OnCollectCoins;
        
        private int _reward;

        public override void Initialize(int reward)
        {
            _reward = reward;
        }

        public int GetCoins() =>
            _repository.Coins;

        public bool EnoughCoins(int coins) => 
            _repository.Coins >= coins;

        public void AddReward() =>
            AddCoins(_reward);

        public void AddCoins(int coins)
        {
            if(coins < 0) return;
            
            _repository.Coins += coins;
            
            OnCollectCoins?.Invoke(_repository.Coins);
        }

        public void Spend(int coins)
        {
            if(!EnoughCoins(coins) || coins < 0) return;
            
            _repository.Coins -= coins;
            
            OnCollectCoins?.Invoke(_repository.Coins);
        }
    }
}