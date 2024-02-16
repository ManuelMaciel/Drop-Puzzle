using System.Collections;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class ShapeAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite deathSprite;

        private SpriteRenderer _spriteRenderer;
        private Coroutine _pulseCoroutine;
        private IParticlesFactory _particlesFactory;

        [Inject]
        public void Construct(IParticlesFactory particlesFactory) =>
            _particlesFactory = particlesFactory;

        private void Awake()
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        public void PlayDeathAnimation()
        {
            ParticleSystem vfx = _particlesFactory.CreateDeathVfx(this.transform.position);
            vfx.startSize = transform.parent.localScale.x - vfx.startSize;
            _spriteRenderer.sprite = deathSprite;
            _spriteRenderer.color = Color.white;
        }

        public void PlayPulseAnimation()
        {
            _pulseCoroutine = StartCoroutine(PulseAnimation());
        }

        public void StopPulseAnimation()
        {
            if (_pulseCoroutine == null) return;

            StopCoroutine(_pulseCoroutine);

            _spriteRenderer.color = Color.white;
        }

        private IEnumerator PulseAnimation()
        {
            float elapsedTime = 0f;

            while (true)
            {
                float lerpValue = Mathf.PingPong(elapsedTime, 1f);
                _spriteRenderer.color = Color.Lerp(Color.red, Color.white, lerpValue);

                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }
    }
}