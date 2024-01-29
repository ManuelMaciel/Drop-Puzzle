using System;
using System.Collections.Generic;

namespace Code.Runtime.Repositories
{
    [Serializable]
    public class GameplayShapesRepository : IRepository
    {
        public List<ShapeData> ShapesData = new List<ShapeData>();
        
        [Serializable]
        public struct ShapeData
        {
            public int Id;
            public Vector3Data Position;
            public ShapeSize ShapeSize;
        }
    }
}