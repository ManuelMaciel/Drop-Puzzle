using System;
using System.Collections.Generic;

namespace Code.Runtime.Repositories
{
    [Serializable]
    public class GameplayShapesRepository : IRepository
    {
        public List<ShapeData> ShapesData = new List<ShapeData>();
        
        [Serializable]
        public class ShapeData
        {
            public string Id;
            public Vector3Data Position;
            public ShapeSize ShapeSize;
        }
    }
}