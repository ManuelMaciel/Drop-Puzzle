using System;
using Code.Runtime.Interactors;
using Code.Services.AudioService;
using Code.Services.Progress;
using CodeBase.Services.LogService;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Gameplay
{
    public class ComboDetector : ITickable, IInitializable, IDisposable
    {
        private const float TimeToEndCombo = 0.5f;

        private int comboCount;
        private float time;

        private ShapeInteractor _shapeInteractor;
        private ScoreInteractor _scoreInteractor;
        private MoneyInteractor _moneyInteractor;

        private ILogService _logService;
        private IPersistentProgressService _progressService;
        private IAudioService _audioService;

        [Inject]
        public void Construct(IPersistentProgressService progressService, ILogService logService, IAudioService audioService)
        {
            _audioService = audioService;
            _progressService = progressService;
            _logService = logService;
        }

        public void Initialize()
        {
            _scoreInteractor = _progressService.InteractorContainer.Get<ScoreInteractor>();
            _shapeInteractor = _progressService.InteractorContainer.Get<ShapeInteractor>();
            _moneyInteractor = _progressService.InteractorContainer.Get<MoneyInteractor>();

            _shapeInteractor.OnShapeCombined += OnShapesCombined;
        }

        public void Dispose()
        {
            if (_shapeInteractor != null)
                _shapeInteractor.OnShapeCombined -= OnShapesCombined;
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
            if (comboCount < 2) return;

            _scoreInteractor.AddScore(comboCount * 5);
            _moneyInteractor.AddCoins(comboCount);
            _audioService.PlayVibrate();

            _logService.Log("End Combo: " + comboCount + "X");
        }

        private void OnShapesCombined(IShapeBase shape)
        {
            comboCount++;

            time = 0;
        }
    }
}