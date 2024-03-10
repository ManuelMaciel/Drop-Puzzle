using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Configs;
using Code.Runtime.Logic.Gameplay;
using Code.Runtime.Services.StaticDataService;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Runtime.Logic.Animation
{
    public class ActiveShapeAnimatorsHandler : MonoBehaviour, IActiveShapeAnimatorsHandler
    {
        private Dictionary<Shape, ShapeAnimator> _shapeAnimators = new();
        
        private IStaticDataService _staticDataService;

        [Inject]
        public void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        private void OnEnable() =>
            StartCoroutine(FindBlinkShapes());

        private void OnDisable() =>
            StopCoroutine(FindBlinkShapes());

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
                AnimationConfig animationConfig = _staticDataService.AnimationConfig;
                Vector2 minMaxBlinkShapes = animationConfig.MinMaxBlinkShapes;

                yield return new WaitForSeconds(animationConfig.BlinkDelay);
                
                for (int i = 0; i < Random.Range(minMaxBlinkShapes.x, minMaxBlinkShapes.y); i++)
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