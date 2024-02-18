using System;
using Code.Runtime.Logic.Gameplay;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class ShapeInteractor : Interactor<ShapeRepository>
    {
        public event Action<IShapeBase> OnShapeCombined;

        public void ShapeCombined(IShapeBase shape)
        {
            _repository.CollectedShapes++;
            
            OnShapeCombined?.Invoke(shape);
        }
    }
}