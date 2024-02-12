using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class TapEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private bool _isPressed;
        private IInput _input;

        [Inject]
        public void Construct(IInput input)
        {
            _input = input;
        }

        private void Update()
        {
            if (_input.IsPress() && !_isPressed)
            {
                ParticleSystem instantiate = Instantiate(_particleSystem);

                instantiate.transform.position = _input.GetPosition();
                instantiate.Play();
                
                _isPressed = true;
            }

            if (_input.IsDropped()) _isPressed = false;
        }
    }
}