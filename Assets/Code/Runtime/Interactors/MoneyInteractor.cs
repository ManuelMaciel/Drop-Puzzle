using System;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class MoneyInteractor : Interactor<MoneyRepository>
    {
        public event Action<int> OnCollectCoins;
        
        public int GetCoins() =>
            _repository.Coins;
        
        public void AddCoins(int coins)
        {
            if(coins < 0) return;
            
            _repository.Coins += coins;
            
            OnCollectCoins?.Invoke(_repository.Coins);
        }
    }
}