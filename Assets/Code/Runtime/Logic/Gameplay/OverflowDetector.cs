using System.Collections;
using System.Collections.Generic;
using Code.Runtime.Configs;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States.Gameplay;
using Code.Services.AudioService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Gameplay
{
    public class OverflowDetector : MonoBehaviour
    {
        private const string ShapeTag = "Shape";

        private Dictionary<Collider2D, Coroutine> timers = new Dictionary<Collider2D, Coroutine>();
        private GameplayStateMachine _gameplayStateMachine;
        private IAudioService _audioService;

        [Inject]
        public void Construct(GameplayStateMachine gameplayStateMachine, IAudioService audioService)
        {
            _audioService = audioService;
            _gameplayStateMachine = gameplayStateMachine;
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(ShapeTag) && !timers.ContainsKey(other))
            {
                timers[other] = StartCoroutine(StartTimerToLose(other));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (timers.ContainsKey(other))
            {
                StopCoroutine(timers[other]);
                other.GetComponent<ShapeAnimator>().StopPulseAnimation();
                timers.Remove(other);
            }
        }

        private IEnumerator StartTimerToLose(Collider2D collider)
        {
            yield return new WaitForSeconds(2f);
        
            ShapeAnimator shapeAnimator = collider.GetComponent<ShapeAnimator>();

            shapeAnimator.PlayPulseAnimation();
        
            yield return new WaitForSeconds(4f);
        
            shapeAnimator.StopPulseAnimation();
            shapeAnimator.PlayDeathAnimation();
            _audioService.PlaySfx(SfxType.ShapeDeath);
        
            _gameplayStateMachine.Enter<LoseState>();
        }
    }
}