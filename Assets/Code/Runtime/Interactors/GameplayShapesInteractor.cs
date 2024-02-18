using System.Collections.Generic;
using Code.Runtime.Extensions;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Gameplay;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class GameplayShapesInteractor : Interactor<GameplayShapesRepository>
    {
        public IEnumerable<GameplayShapesRepository.ShapeData> GetShapesData()
            => _repository.ShapesData;

        public void ClearShapesData() => 
            _repository.ShapesData.Clear();

        public void AddShape(Shape shape)
        {
            var shapeData = new GameplayShapesRepository.ShapeData()
            {
                Id = shape.ShapeId,
                Position = shape.transform.position.AsVectorData(),
                ShapeSize = shape.ShapeSize
            };

            if(_repository.ShapesData.Contains(shapeData)) return;

            _repository.ShapesData.Add(shapeData);
        }

        public void RemoveShape(IShapeBase shapeBase)
        {
            _repository.ShapesData.RemoveAll(data => data.Id == shapeBase.ShapeId);
        }

        public void UpdateShapeData(IShapeBase shapeBase)
        {
            GameplayShapesRepository.ShapeData existingShapeData = _repository.ShapesData.Find(data => data.Id == shapeBase.ShapeId);
            
            if (existingShapeData != null)
            {
                existingShapeData.Position = shapeBase.transform.position.AsVectorData();
                existingShapeData.ShapeSize = shapeBase.ShapeSize;
            }
        }
    }
}