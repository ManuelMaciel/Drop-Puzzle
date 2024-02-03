using System.Collections.Generic;
using Code.Runtime.Extensions;
using Code.Runtime.Logic;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class GameplayShapesInteractor : Interactor<GameplayShapesRepository>
    {
        public IEnumerable<GameplayShapesRepository.ShapeData> GetShapesData()
            => _repository.ShapesData;

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

        public void RemoveShape(Shape shape)
        {
            _repository.ShapesData.RemoveAll(data => data.Id == shape.ShapeId);
        }

        public void UpdateShapeData(Shape shape)
        {
            GameplayShapesRepository.ShapeData existingShapeData = _repository.ShapesData.Find(data => data.Id == shape.ShapeId);
            
            if (existingShapeData != null)
            {
                existingShapeData.Position = shape.transform.position.AsVectorData();
                existingShapeData.ShapeSize = shape.ShapeSize;
            }
        }
    }
}