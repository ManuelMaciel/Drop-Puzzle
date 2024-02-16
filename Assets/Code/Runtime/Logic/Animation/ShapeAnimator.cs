using System;
using System.Collections;
using CodeBase.Services.StaticDataService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    [RequireComponent(typeof(Shape))]
    public class ShapeAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite deathSprite;
        [SerializeField] private SpriteRenderer shapeSpriteRenderer;

        private IParticlesFactory _particlesFactory;
        private IStaticDataService _staticDataService;
        private IActiveShapeAnimatorsHandler _activeShapeAnimatorsHandler;

        private Coroutine _pulseCoroutine;
        private Shape _shape;
        private bool _deathAnimationPlayed;

        [Inject]
        public void Construct(IParticlesFactory particlesFactory, IStaticDataService staticDataService,
            IActiveShapeAnimatorsHandler activeShapeAnimatorsHandler)
        {
            _activeShapeAnimatorsHandler = activeShapeAnimatorsHandler;
            _staticDataService = staticDataService;
            _particlesFactory = particlesFactory;
        }

        private void Awake() =>
            _shape = this.GetComponent<Shape>();

        private void OnEnable()
        {
            shapeSpriteRenderer.sprite = _staticDataService.ShapeSizeConfig.Sprites[(int)_shape.ShapeSize];
            
            _activeShapeAnimatorsHandler.AddShapeAnimator(_shape, this);
        }

        private void OnDisable() =>
            _activeShapeAnimatorsHandler.RemoveShapeAnimator(_shape);

        public void PlayDeathAnimation()
        {
            ParticleSystem vfx = _particlesFactory.CreateDeathVfx(this.transform.position);
            vfx.startSize = transform.localScale.x - vfx.startSize;
            shapeSpriteRenderer.sprite = deathSprite;
            shapeSpriteRenderer.color = Color.white;
            _deathAnimationPlayed = true;
        }

        public void PlayBlinkAnimation()
        {
            StartCoroutine(BlinkAnimation());
        }

        public void PlayPulseAnimation()
        {
            _pulseCoroutine = StartCoroutine(PulseAnimation());
        }

        public void StopPulseAnimation()
        {
            if (_pulseCoroutine == null) return;

            StopCoroutine(_pulseCoroutine);

            shapeSpriteRenderer.color = Color.white;
        }

        private IEnumerator PulseAnimation()
        {
            float elapsedTime = 0f;

            while (true)
            {
                float lerpValue = Mathf.PingPong(elapsedTime, 1f);
                shapeSpriteRenderer.color = Color.Lerp(Color.red, Color.white, lerpValue);

                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }

        private IEnumerator BlinkAnimation()
        {
            Sprite startSprite = shapeSpriteRenderer.sprite;

            shapeSpriteRenderer.sprite = _staticDataService.ShapeSizeConfig.BlinkSprites[(int)_shape.ShapeSize];

            yield return new WaitForSeconds(1f);

            if (!_deathAnimationPlayed)
                shapeSpriteRenderer.sprite = startSprite;
        }
    }
}