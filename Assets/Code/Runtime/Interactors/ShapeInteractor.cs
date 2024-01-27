using System;
using Code.Runtime.Repositories;

namespace Code.Runtime.Interactors
{
    public class ShapeInteractor : Interactor<ShapeRepository>
    {
        public event Action OnShapeCombined;

        public void ShapeCombined()
        {
            _repository.CollectedShapes++;
            
            OnShapeCombined?.Invoke();
        }
    }
}