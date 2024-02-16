using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class TapEffect : MonoBehaviour
    {
        private IParticlesFactory _particlesFactory;
        private IInput _input;
        private bool _isPressed;

        [Inject]
        public void Construct(IInput input, IParticlesFactory particlesFactory)
        {
            _particlesFactory = particlesFactory;
            _input = input;
        }
        
        private void Update()
        {
            if (_input.IsPress() && !_isPressed)
            {
                _particlesFactory.CreateTapVfx(_input.GetPosition());
                
                _isPressed = true;
            }

            if (_input.IsDropped()) _isPressed = false;
        }
    }
}