using System.Collections;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.StateMachines;
using Code.Runtime.Infrastructure.States;
using Code.Runtime.Infrastructure.States.Gameplay;
using UnityEngine;
using Zenject;

public class OverflowDetector : MonoBehaviour
{
    private const string ShapeTag = "Shape";

    private Dictionary<Collider2D, Coroutine> timers = new Dictionary<Collider2D, Coroutine>();
    private GameplayStateMachine _gameplayStateMachine;

    [Inject]
    public void Construct(GameplayStateMachine gameplayStateMachine)
    {
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
            timers.Remove(other);
        }
    }

    private IEnumerator StartTimerToLose(Collider2D collider)
    {
        yield return new WaitForSeconds(2f);

        SpriteRenderer spriteRenderer = collider.GetComponentInChildren<SpriteRenderer>();

        float flashDuration = 4f;
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration)
        {
            float lerpValue = Mathf.PingPong(elapsedTime, 1f);
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, lerpValue);

            yield return null;
            elapsedTime += Time.deltaTime;
        }
        
        _gameplayStateMachine.Enter<LoseState>();
    }
}