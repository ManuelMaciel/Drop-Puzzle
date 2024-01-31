using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Extensions;
using Code.Runtime.Logic;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class GameplayShapesInteractor : Interactor<GameplayShapesRepository>, IUpdatebleProgress
    {
        private Dictionary<Shape, GameplayShapesRepository.ShapeData> _shapesData =
            new Dictionary<Shape, GameplayShapesRepository.ShapeData>();

        public IEnumerable<GameplayShapesRepository.ShapeData> GetShapesData()
            => _repository.ShapesData;

        public void AddShape(Shape shape)
        {
            var shapeData = new GameplayShapesRepository.ShapeData()
            {
                Id = shape.GetInstanceID(),
                Position = shape.transform.position.AsVectorData(),
                ShapeSize = shape.ShapeSize
            };

            _shapesData.Add(shape, shapeData);
        }

        public void RemoveShape(Shape shape)
        {
            _shapesData.Remove(shape);
        }

        public void UpdateProgress()
        {
            _repository.ShapesData = _shapesData.ToDictionary(
                k => k.Key,
                d => new GameplayShapesRepository.ShapeData()
                {
                    Position = d.Key.transform.position.AsVectorData(),
                    ShapeSize = d.Value.ShapeSize
                }).Values.ToList();
        }
    }
}