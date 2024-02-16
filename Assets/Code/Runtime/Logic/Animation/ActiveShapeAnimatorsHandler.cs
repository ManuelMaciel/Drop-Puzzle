using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Runtime.Logic.Animation
{
    //TODO: Change in MonoBehaviour
    public class ActiveShapeAnimatorsHandler : IActiveShapeAnimatorsHandler
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private Dictionary<Shape, ShapeAnimator> _shapeAnimators = new();
        private Coroutine _findBlinkShapesCoroutine;

        public ActiveShapeAnimatorsHandler(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            
            _findBlinkShapesCoroutine = _coroutineRunner.StartCoroutine(FindBlinkShapes());
        }

        public void AddShapeAnimator(Shape shape, ShapeAnimator shapeAnimator)
        {
            _shapeAnimators.Add(shape, shapeAnimator);
        }

        public void RemoveShapeAnimator(Shape shape)
        {
            _shapeAnimators.Remove(shape);
        }

        private IEnumerator FindBlinkShapes()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);

                for (int i = 0; i < Random.Range(1, 3); i++)
                    GetRandomShapeAnimator().Value.PlayBlinkAnimation();
            }
        }

        private KeyValuePair<Shape, ShapeAnimator> GetRandomShapeAnimator()
        {
            int randomIndex = Random.Range(0, _shapeAnimators.Count);

            KeyValuePair<Shape, ShapeAnimator> randomPair = _shapeAnimators.ElementAt(randomIndex);

            return randomPair;
        }
    }
}