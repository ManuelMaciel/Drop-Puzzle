using System;
using System.Collections;
using Code.Runtime.Infrastructure.ObjectPool;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class TapEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private bool _isPressed;
        private IInput _input;
        private IGlobalGameObjectPool _globalGameObjectPool;
        private IGameObjectPool<ParticleSystem> _gameObjectPool;

        [Inject]
        public void Construct(IInput input, IGlobalGameObjectPool globalGameObjectPool)
        {
            _globalGameObjectPool = globalGameObjectPool;
            _input = input;
        }

        private void Start()
        {
            _gameObjectPool = new GameObjectPool<ParticleSystem>(_particleSystem, 5, _globalGameObjectPool);
            _gameObjectPool.Initialize();
        }

        private void Update()
        {
            if (_input.IsPress() && !_isPressed)
            {
                ParticleSystem instantiate = _gameObjectPool.Get(_input.GetPosition());
                instantiate.Play();
                StartCoroutine(CheckIfPlay(instantiate, () => _gameObjectPool.Return(instantiate)));
                
                _isPressed = true;
            }

            if (_input.IsDropped()) _isPressed = false;
        }

        IEnumerator CheckIfPlay(ParticleSystem ps, Action onPlayed)
        {
            while (ps != null)
            {
                yield return new WaitForSeconds(0.5f);
                if (!ps.IsAlive(true))
                {
                    onPlayed?.Invoke();
                    break;
                }
            }
        }
    }
}