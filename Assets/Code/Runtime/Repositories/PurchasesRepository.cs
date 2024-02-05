using System;
using System.Collections.Generic;
using Code.Runtime.Configs;

namespace Code.Runtime.Repositories
{
    [Serializable]
    public class PurchasesRepository : IRepository
    {
        public List<BackgroundType> PurchasedBackgrounds = new List<BackgroundType>();
    }
}