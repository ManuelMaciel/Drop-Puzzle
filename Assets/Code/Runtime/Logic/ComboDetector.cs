using Code.Runtime.Interactors;
using Code.Services.Progress;
using CodeBase.Services.LogService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic
{
    public class ComboDetector : ITickable
    {
        private const float TimeToEndCombo = 0.5f;

        private readonly ScoreInteractor _scoreInteractor;
        private readonly ILogService _logService;

        private int comboCount;
        private float time;

        public ComboDetector(IPersistentProgressService progressService, ILogService logService)
        {
            _logService = logService;
            _scoreInteractor = progressService.InteractorContainer.Get<ScoreInteractor>();

            _scoreInteractor.OnScoreIncreased += ShapesCombined;
        }

        private void ShapesCombined(int score)
        {
            comboCount++;

            time = 0;
        }

        public void Tick()
        {
            if (time >= TimeToEndCombo)
            {
                EndCombo();
                
                comboCount = 0;
            }
            else time += Time.deltaTime;
        }

        private void EndCombo()
        {
            if(comboCount < 2) return;
            
            _logService.Log("End Combo: " + comboCount + "X");
        }
    }
}