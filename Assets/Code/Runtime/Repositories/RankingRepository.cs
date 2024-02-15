using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Code.Runtime.Repositories
{
    [Serializable]
    public class RankingRepository : IRepository
    {
        public List<RecordData> RecordsData = new List<RecordData>();
    }

    [Serializable]
    public class RecordData
    {
        public DateTime RecordDate;
        public int Score;
    }
}